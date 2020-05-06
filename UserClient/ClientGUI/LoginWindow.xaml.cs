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
            UserData = textBoxEmail.Text + ", " + passwordBox1.Password;
            DialogResult = true;
            Close();
        }
    }
}
