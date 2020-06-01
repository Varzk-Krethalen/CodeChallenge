using System;
using System.Windows;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;
        private double previousWidth = 800;
        private double previousHeight = 450;

        public MainWindow()
        {
            InitializeComponent();
            SetVisibility(false);
            viewModel = DataContext as MainViewModel;
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
            if (viewModel.RequestLogin())
            {
                SetVisibility(true);
                viewModel.RefreshChallenges();
            }
            else
            {
                Close();
            }
        }

        private void Refresh_Challenges(object sender, RoutedEventArgs e)
        {
            viewModel.RefreshChallenges();
        }

        private void Load_Challenge(object sender, RoutedEventArgs e)
        {
            viewModel.LoadSelectedChallenge();
            currentChallengeTab.IsSelected = true; //TODO: Only if succeessful
            //switch to challenge window
        }

        private void Submit_Challenge(object sender, RoutedEventArgs e)
        {
            viewModel.SubmitChallenge();
        }

        private void Log_Out(object sender, RoutedEventArgs e)
        {
            //TODO: Add logout system.
        }

        private void Add_Challenge(object sender, RoutedEventArgs e)
        {
            ChallengeEditorDialog challengeEditor = new ChallengeEditorDialog();
            if (challengeEditor.ShowDialog() == true)
            {
                viewModel.AddChallenge(challengeEditor.Challenge);
            }
        }

        private void Edit_Challenge(object sender, RoutedEventArgs e)
        {
            ChallengeEditorDialog challengeEditor = new ChallengeEditorDialog(viewModel.Model, viewModel.SelectedChallenge); //view shouldn't know about model
            if (challengeEditor.ShowDialog() == true)
            {
                viewModel.EditSelectedChallenge(challengeEditor.Challenge);
            }
        }

        private void Delete_Challenge(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                viewModel.DeleteSelectedChallenge();
            }
        }
    }
}
