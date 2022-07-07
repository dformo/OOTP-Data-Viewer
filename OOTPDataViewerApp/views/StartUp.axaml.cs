using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using OOTPDataViewerApp.viewmodels;
using OOTPDataViewerDataSource;
using System;
using System.IO;

namespace OOTPDataViewerApp.views
{
    public partial class StartUp : UserControl
    {
        public StartUp()
        {
            InitializeComponent();
            this.DataContext = new StartUpVM();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private async void FolderChooserClick(object sender, RoutedEventArgs e)
        {
            this.FindControl<TextBlock>("lblError").Text = string.Empty;
            if (this.Parent == null) { return; }
            var mainWindow = (Window)this.Parent;

            var folderDialog = new OpenFolderDialog()
            {
                Title = "Select game location",
                Directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            var selectedFolder = await folderDialog.ShowAsync(mainWindow);
            if (!string.IsNullOrEmpty(selectedFolder))
            {
                LoadGameFile(mainWindow, selectedFolder);
            }

            e.Handled = true;
        }

        private void LastUsedClick(object sender, PointerPressedEventArgs e)
        {
            this.FindControl<TextBlock>("lblError").Text = string.Empty;
            if (this.Parent == null) { return; }
            var mainWindow = (Window)this.Parent;

            if (e.Source != null && this.DataContext != null && e.Source is TextBlock)
            {
                LoadGameFile(mainWindow, ((StartUpVM)this.DataContext).GetGameLocation());
            }

            e.Handled = true;
        }

        private void LoadGameFile(Window mainWindow, string? selectedFolder)
        {
            try
            {
                if (this.DataContext == null) { return; }
                if (selectedFolder == null) { return; }
                var game = new GameData(selectedFolder);
                ((StartUpVM)this.DataContext).SetLastUsedGameFile(selectedFolder);
                mainWindow.Content = new Main() { DataContext = new MainVM(game) };
            }
            catch (DirectoryNotFoundException ex)
            {
                this.FindControl<TextBlock>("lblError").Text = ex.Message;
            }
            catch { throw; }
        }
    }
}
