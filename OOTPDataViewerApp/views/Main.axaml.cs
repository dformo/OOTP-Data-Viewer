using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using OOTPDataViewerDataSource;

namespace OOTPDataViewerApp.views
{
    public partial class Main : UserControl
    {
        public Main()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public GameData? GameDataSource { get; set; }

        public void LoadView()
        {
            if (GameDataSource != null)
            {
                this.FindControl<TextBlock>("lblResult").Text = GameDataSource.GetGameLocation();
            }
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.Parent == null) { return; }
            var mainWindow = (Window)this.Parent;
            mainWindow.Content = new StartUp();
        }
    }
}
