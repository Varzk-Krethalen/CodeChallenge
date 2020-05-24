using System.Windows;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Load_Challenge(object sender, RoutedEventArgs e)
        {
            ((MainViewModel)DataContext).RetrieveChallenge();
        }

        private void Submit_Challenge(object sender, RoutedEventArgs e)
        {
            ((MainViewModel)DataContext).SubmitChallenge();
        }

        private void Log_Out(object sender, RoutedEventArgs e)
        {
            //TODO: Add logout system.
        }
    }
}
