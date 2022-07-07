using ReactiveUI;
using System.Configuration;
using System.IO;

namespace OOTPDataViewerApp.viewmodels
{
    public class StartUpVM : ReactiveObject
    {
        private const string APP_SETTING_LAST_USED_FILE = "LastUsedFile";
        private string? lastUsedFile;

        public StartUpVM()
        {
            lastUsedFile = null;
            var settings = ConfigurationManager.AppSettings;
            if (settings[APP_SETTING_LAST_USED_FILE] != null)
            {
                lastUsedFile = settings[APP_SETTING_LAST_USED_FILE];
            }
        }

        public bool HasLastUsedFile { get => lastUsedFile != null; }

        public string? LastUsedFilePath
        {
            get
            {
                if (lastUsedFile == null)
                    return lastUsedFile;

                var dirInfo = new DirectoryInfo(lastUsedFile);
                return Path.GetDirectoryName(dirInfo.FullName);
            }
        }

        public string? LastUsedFileName
        {
            get
            {
                if (lastUsedFile == null)
                    return lastUsedFile;

                var dirInfo = new DirectoryInfo(lastUsedFile);
                return dirInfo.Name;
            }
        }

        public string? GetGameLocation()
        {
            return lastUsedFile;
        }

        public void SetLastUsedGameFile(string gameLocation)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings[APP_SETTING_LAST_USED_FILE] == null)
                config.AppSettings.Settings.Add(APP_SETTING_LAST_USED_FILE, gameLocation);
            else
                config.AppSettings.Settings[APP_SETTING_LAST_USED_FILE].Value = gameLocation;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            lastUsedFile = gameLocation;
        }
    }
}
