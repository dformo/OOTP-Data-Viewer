using Avalonia.Controls;
using OOTPDataViewerDataSource;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

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
