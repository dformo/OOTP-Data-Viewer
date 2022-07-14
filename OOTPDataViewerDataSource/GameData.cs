namespace OOTPDataViewerDataSource
{
    public class GameData
    {
        private const string DUMP_FOLDER = "dump";
        private const string CSV_FOLDER = "csv";

        private string gameLocation = string.Empty;
        private string dumpLocation = string.Empty;
        private List<string> dumpLocationList;

        private void SetGameLocation(string gameLocation)
        {
            if (!Directory.Exists(gameLocation)) { throw new DirectoryNotFoundException(); }

            var dirInfo = new DirectoryInfo(gameLocation);
            if (Directory.Exists(Path.Combine(dirInfo.FullName, DUMP_FOLDER)))
            {
                this.gameLocation = dirInfo.FullName;
            }
            else if (dirInfo.Name == DUMP_FOLDER && dirInfo.Parent != null && Directory.Exists(dirInfo.Parent.FullName))
            {
                this.gameLocation = dirInfo.Parent.FullName;
            }
            else if (dirInfo.Parent != null && dirInfo.Parent.Name == DUMP_FOLDER && dirInfo.Parent.Parent != null && Directory.Exists(dirInfo.Parent.Parent.FullName))
            {
                this.gameLocation = dirInfo.Parent.Parent.FullName;
            }
            else if (dirInfo.Name == CSV_FOLDER
                && dirInfo.Parent != null && dirInfo.Parent.Parent != null && dirInfo.Parent.Parent.Parent != null
                && dirInfo.Parent.Parent.Name == DUMP_FOLDER
                && Directory.Exists(dirInfo.Parent.Parent.Parent.FullName))
            {
                this.gameLocation = dirInfo.Parent.Parent.Parent.FullName;
                this.dumpLocation = dirInfo.Parent.Parent.FullName;
            }
            else
            {
                throw new DirectoryNotFoundException("Unable to find the 'dump' folder");
            }
        }

        public GameData(string gameLocation)
        {
            SetGameLocation(gameLocation);

            dumpLocationList = new List<string>(Directory.GetDirectories(Path.Combine(gameLocation, DUMP_FOLDER), "dump_*", SearchOption.TopDirectoryOnly))
                                                .OrderByDescending(x => x)
                                                .ToList();
            
            if (dumpLocationList.Count > 0 && string.IsNullOrEmpty(dumpLocation))
                dumpLocation = dumpLocationList[0];
        }

        public string GetGameLocation()
        {
            return gameLocation;
        }

        public string GetDumpLocation()
        {
            return dumpLocation;
        }
    }
}