using ClientModels.Interfaces;
using System.Windows;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class LoginDialog : Window
    {
        public IUser User { get; set; }
        public string Username { get; set; }
        public string Pass { get; set; }
        public IModel Model { get; }

        public LoginDialog(IModel model)
        {
            InitializeComponent();
            Model = model;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Username = usernameBox.Text;
            Pass = passwordBox.Password;

            if (Model.ValidateUser(Username, Pass, out IUser user))
            {
                User = user;
                DialogResult = true;
                Close();
            }
            else
            {
                errormessage.Text = "Invalid User Details";
            }
        }
    }
}
