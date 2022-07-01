namespace OOTPDataViewerDataSource
{
    public class GameData
    {
        private const string DUMP_FOLDER = "dump";
        private string gameLocation = string.Empty;

        public GameData(string gameLocation)
        {
            SetGameLocation(gameLocation);
        }

        public void SetGameLocation(string gameLocation)
        {
            if (!Directory.Exists(gameLocation)) { throw new DirectoryNotFoundException(); }

            var dirInfo = new DirectoryInfo(gameLocation);
            if (dirInfo.Name == DUMP_FOLDER && dirInfo.Parent != null)
            {
                this.gameLocation = dirInfo.Parent.FullName;
            }
            else if (Directory.Exists(Path.Combine(dirInfo.FullName, DUMP_FOLDER)))
            {
                this.gameLocation = dirInfo.FullName;
            }
            else { throw new DirectoryNotFoundException("Unable to find the 'dump' folder"); }
        }

        public string GetGameLocation()
        {
            return gameLocation;
        }
    }
}