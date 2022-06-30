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

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.Parent == null) { return; }
            var mainWindow = (Window)this.Parent;
            mainWindow.Content = new StartUp();
        }
    }
}
