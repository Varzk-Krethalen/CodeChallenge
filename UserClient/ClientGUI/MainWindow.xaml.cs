﻿using ClientGUI.Dialogs;
using ClientModels.Interfaces;
using System.Text.RegularExpressions;
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
                viewModel.RefreshChallenges(); //TODO: move into the function on the viewmodel side
                viewModel.RefreshUsers();
                viewModel.GetAllChallengeRanking();
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
            if (viewModel.LoadSelectedChallenge())
            {
                currentChallengeTab.IsSelected = true;
            }
        }

        private void Submit_Challenge(object sender, RoutedEventArgs e)
        {
            viewModel.SubmitChallenge((result) =>
            {
                if (result)
                {
                    MessageBox.Show($"You have completed {viewModel.SelectedChallenge.Language} challenge {viewModel.SelectedChallenge.Name}!",
                                    $"Completion of challenge {viewModel.SelectedChallenge.ChallengeID}",
                                    MessageBoxButton.OK);
                    challengesListTab.IsSelected = true;
                }
            });
        }

        private void Log_Out(object sender, RoutedEventArgs e)
        {
            viewModel.Logout();
            Close();
        }

        private void Add_Challenge(object sender, RoutedEventArgs e)
        {
            ChallengeEditorDialog challengeEditor = new ChallengeEditorDialog(viewModel.Model); //TODO: view shouldn't know about model
            if (challengeEditor.ShowDialog() == true)
            {
                viewModel.AddChallenge(challengeEditor.Challenge);
            }
        }

        private void Edit_Challenge(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedChallenge != null)
            { 
                ChallengeEditorDialog challengeEditor = new ChallengeEditorDialog(viewModel.Model, viewModel.SelectedChallenge);
                if (challengeEditor.ShowDialog() == true)
                {
                    viewModel.EditSelectedChallenge(challengeEditor.Challenge);
                }
            }
        }

        private void Delete_Challenge(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedChallenge != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", $"Delete Challenge: {viewModel.SelectedChallenge.Name}", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    viewModel.DeleteSelectedChallenge();
                }
            }
        }

        private void Refresh_Users(object sender, RoutedEventArgs e)
        {
            viewModel.RefreshUsers();
        }

        private void Add_User(object sender, RoutedEventArgs e)
        {
            UserEditorDialog userEditor = new UserEditorDialog(viewModel.Model); //TODO: view shouldn't know about model
            if (userEditor.ShowDialog() == true)
            {
                viewModel.AddUser(userEditor.User, userEditor.NewPassword);
            }
        }

        private void Edit_User(object sender, RoutedEventArgs e)
        {
            UserEditorDialog userEditor = new UserEditorDialog(viewModel.Model, viewModel.SelectedUser); //TODO: view shouldn't know about model
            if (userEditor.ShowDialog() == true)
            {
                IUser user = userEditor.User;
                switch (userEditor.EditedField)
                {
                    case UserEditorDialog.UserField.NAME:
                        viewModel.SaveUserName(user.UserID, user.Username);
                        break;
                    case UserEditorDialog.UserField.PASS:
                        viewModel.SaveUserPass(user.UserID, userEditor.NewPassword);
                        break;
                    case UserEditorDialog.UserField.TYPE:
                        viewModel.SaveUserType(user.UserID, user.UserType);
                        break;
                }
            }
        }

        private void Delete_User(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedUser != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", $"Delete User: {viewModel.SelectedUser.Username}", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    viewModel.DeleteSelectedUser();
                }
            }
        }

        private void Get_Challenge_Ranking(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(rankingChallengeID.Text))
            {
                viewModel.GetAllChallengeRanking();
            }
            else
            {
                viewModel.GetChallengeRanking(long.Parse(rankingChallengeID.Text));
            }
        }

        private void Get_User_Ranking(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(rankingUserID.Text))
            {
                viewModel.GetUserRanking(long.Parse(rankingUserID.Text));
            }
        }

        private void TextBlock_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
