using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
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
                try
                {
                    var game = new GameData(selectedFolder);
                    var vwMain = new Main() { GameDataSource = game };
                    vwMain.LoadView();
                    mainWindow.Content = vwMain;
                }
                catch (DirectoryNotFoundException ex)
                {
                    this.FindControl<TextBlock>("lblError").Text = ex.Message;
                }
                catch { throw; }
            }
        }
    }
}
