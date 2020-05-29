using System.Windows;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class LoginDialog : Window
    {
        public string UserData { get; set; }

        public LoginDialog()
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
