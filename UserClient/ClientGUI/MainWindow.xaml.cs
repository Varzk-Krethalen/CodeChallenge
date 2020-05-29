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
            currentChallengeTab.IsSelected = true; //TODO: Only if succeessful
            //switch to challenge window
        }

        private void Submit_Challenge(object sender, RoutedEventArgs e)
        {
            ((MainViewModel)DataContext).SubmitChallenge();
        }

        private void Log_Out(object sender, RoutedEventArgs e)
        {
            //TODO: Add logout system.
        }

        private void Add_Challenge(object sender, RoutedEventArgs e)
        {
            //open popup
        }

        private void Edit_Challenge(object sender, RoutedEventArgs e)
        {
            //open popup - same as adding?
        }

        private void Delete_Challenge(object sender, RoutedEventArgs e)
        {
            //confirmation dialog, then do a delete
        }
    }
}
