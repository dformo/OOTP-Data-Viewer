namespace OOTPDataViewerDataSource
{
    public class GameData
    {
        private string gameLocation = string.Empty;

        public GameData(string gameLocation)
        {
            SetGameLocation(gameLocation);
        }

        public void SetGameLocation(string gameLocation)
        {
            if (!Directory.Exists(gameLocation)) { throw new DirectoryNotFoundException(); }
            
            var dirInfo = new DirectoryInfo(gameLocation);
            if (dirInfo.Name == "dump" && dirInfo.Parent != null)
            {
                dirInfo = dirInfo.Parent;
            }
            var dataFolder = dirInfo.GetDirectories("dump");

            if (dataFolder == null || dataFolder.Length != 1) { throw new DirectoryNotFoundException("Unable to find the 'dump' folder."); }

            this.gameLocation = dataFolder[0].FullName;
        }

        public string GetGameLocation()
        {
            return gameLocation;
        }
    }
}