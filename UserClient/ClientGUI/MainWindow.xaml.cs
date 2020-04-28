using System;
using System.Windows;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Userdata { get; set; }

        public MainWindow()
        {
            RequestLogin();
            InitializeComponent();
            //Show();
            textblock.Text = Userdata;
        }

        private void RequestLogin()
        {
            LoginWindow loginWindow = new LoginWindow { DataContext = this };
            loginWindow.ShowDialog();
            Userdata = loginWindow.UserData;
        }
    }
}
