using System;
using System.Windows;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double previousWidth = 800;
        private double previousHeight = 450;

        public MainWindow()
        {
            InitializeComponent();
            SetVisibility(false);
        }

        private void SetVisibility(bool visible)
        {
            if (!visible) 
            {
                previousWidth = Width;
                previousHeight = Height;
            }
            else
            {
                Activate();
            }
            Width = visible ? previousWidth : 0;
            Height = visible ? previousHeight : 0;
            WindowStyle = visible ? WindowStyle.SingleBorderWindow : WindowStyle.None;
            ShowInTaskbar = visible;
            ShowActivated = visible;
        }

        //TODO: consider doing by App.xaml.cs doing the login bit first
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (((MainViewModel)DataContext).RequestLogin())
            {
                SetVisibility(true);
            }
            else
            {
                Close();
            }
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
