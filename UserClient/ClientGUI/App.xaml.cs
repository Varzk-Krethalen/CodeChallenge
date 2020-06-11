using ClientModels.Interfaces;
using ClientModels.RemoteModelObjects;
using System.Windows;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            IModel model = new RemoteModel("http://localhost:59876/");
            MainViewModel viewModel = new MainViewModel(model);
            MainWindow mainWindow = new MainWindow(viewModel);
            mainWindow.Show();
        }
    }
}
