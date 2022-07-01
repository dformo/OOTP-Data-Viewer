using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using OOTPDataViewerApp.viewmodels;
using OOTPDataViewerDataSource;
using System;
using System.Configuration;
using System.IO;

namespace OOTPDataViewerApp.views
{
    public partial class StartUp : UserControl
    {
        private const string APP_SETTING_LAST_USED_FILE = "LastUsedFile";

        public StartUp()
        {
            InitializeComponent();

            TextBlock lastUsedLabel = this.FindControl<TextBlock>("lblLastUsed");
            TextBlock lastUsed = this.FindControl<TextBlock>("lblLastUsedFile");
            
            var settings = ConfigurationManager.AppSettings;
            if (settings[APP_SETTING_LAST_USED_FILE] != null)
            {
                lastUsedLabel.Text = "Last Used:";
                lastUsed.Text = settings[APP_SETTING_LAST_USED_FILE];
            }
            else
            {
                lastUsedLabel.IsVisible = false;
                lastUsed.IsVisible = false;
            }
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

            if (e.Source != null && e.Source is TextBlock)
            {
                LoadGameFile(mainWindow, ((TextBlock)e.Source).Text);
            }

            e.Handled = true;
        }

        private void LoadGameFile(Window mainWindow, string selectedFolder)
        {
            try
            {
                var game = new GameData(selectedFolder);
                mainWindow.Content = new Main() { DataContext = new MainVM(game) };

                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (config.AppSettings.Settings[APP_SETTING_LAST_USED_FILE] == null)
                    config.AppSettings.Settings.Add(APP_SETTING_LAST_USED_FILE, selectedFolder);
                else
                    config.AppSettings.Settings[APP_SETTING_LAST_USED_FILE].Value = selectedFolder;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (DirectoryNotFoundException ex)
            {
                this.FindControl<TextBlock>("lblError").Text = ex.Message;
            }
            catch { throw; }
        }
    }
}
