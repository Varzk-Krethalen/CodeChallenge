using System.Windows;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public string UserData { get; set; }

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            UserData = usernameBox.Text + ", " + passwordBox.Password;
            DialogResult = true;
            Close();
        }
    }
}
