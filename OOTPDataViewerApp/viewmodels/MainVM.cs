using OOTPDataViewerDataSource;
using ReactiveUI;
using System.Reactive;

namespace OOTPDataViewerApp.viewmodels
{
    public class MainVM : ReactiveObject
    {
        GameData gameData;
        public MainVM(GameData gameData)
        {
            this.gameData = gameData;
            CommandExample = ReactiveCommand.Create(CommandExampleMethod);
        }

        public ReactiveCommand<Unit,Unit> CommandExample { get; }
        private void CommandExampleMethod()
        {

        }

        public string GameLocation { get { return gameData.GetGameLocation(); } }
    }
}
