using ClientGUI.Dialogs;
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
        private MainViewModel ViewModel { get; set; }
        private double previousWidth = 800;
        private double previousHeight = 450;

        public MainWindow(MainViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = viewModel;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ViewModel.RequestLogin())
            {
                SetVisibility(true);
            }
            else
            {
                Close();
            }
        }

        private void Refresh_Challenges(object sender, RoutedEventArgs e)
        {
            ViewModel.RefreshChallenges();
        }

        private void Load_Challenge(object sender, RoutedEventArgs e)
        {
            if (ViewModel.LoadSelectedChallenge())
            {
                currentChallengeTab.IsSelected = true;
            }
        }

        private void Submit_Challenge(object sender, RoutedEventArgs e)
        {
            ViewModel.SubmitChallenge((result) =>
            {
                if (result)
                {
                    MessageBox.Show($"You have completed {ViewModel.SelectedChallenge.Language} challenge {ViewModel.SelectedChallenge.Name}!",
                                    $"Completion of challenge {ViewModel.SelectedChallenge.ChallengeID}",
                                    MessageBoxButton.OK);
                    challengesListTab.IsSelected = true;
                }
            });
        }

        private void Log_Out(object sender, RoutedEventArgs e)
        {
            ViewModel.Logout();
            Close();
        }

        private void Add_Challenge(object sender, RoutedEventArgs e)
        {
            ChallengeEditorDialog challengeEditor = new ChallengeEditorDialog(ViewModel.Model); //TODO: view shouldn't know about model
            if (challengeEditor.ShowDialog() == true)
            {
                ViewModel.AddChallenge(challengeEditor.Challenge);
            }
        }

        private void Edit_Challenge(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedChallenge != null)
            { 
                ChallengeEditorDialog challengeEditor = new ChallengeEditorDialog(ViewModel.Model, ViewModel.SelectedChallenge);
                if (challengeEditor.ShowDialog() == true)
                {
                    ViewModel.EditSelectedChallenge(challengeEditor.Challenge);
                }
            }
        }

        private void Delete_Challenge(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedChallenge != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", $"Delete Challenge: {ViewModel.SelectedChallenge.Name}", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    ViewModel.DeleteSelectedChallenge();
                }
            }
        }

        private void Refresh_Users(object sender, RoutedEventArgs e)
        {
            ViewModel.RefreshUsers();
        }

        private void Add_User(object sender, RoutedEventArgs e)
        {
            UserEditorDialog userEditor = new UserEditorDialog(ViewModel.Model);
            if (userEditor.ShowDialog() == true)
            {
                ViewModel.AddUser(userEditor.User, userEditor.NewPassword);
            }
        }

        private void Edit_User(object sender, RoutedEventArgs e)
        {
            UserEditorDialog userEditor = new UserEditorDialog(ViewModel.Model, ViewModel.SelectedUser);
            if (userEditor.ShowDialog() == true)
            {
                IUser user = userEditor.User;
                switch (userEditor.EditedField)
                {
                    case UserEditorDialog.UserField.NAME:
                        ViewModel.SaveUserName(user.UserID, user.Username);
                        break;
                    case UserEditorDialog.UserField.PASS:
                        ViewModel.SaveUserPass(user.UserID, userEditor.NewPassword);
                        break;
                    case UserEditorDialog.UserField.TYPE:
                        ViewModel.SaveUserType(user.UserID, user.UserType);
                        break;
                }
            }
        }

        private void Delete_User(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedUser != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", $"Delete User: {ViewModel.SelectedUser.Username}", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    ViewModel.DeleteSelectedUser();
                }
            }
        }

        private void Get_Challenge_Ranking(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(rankingChallengeID.Text))
            {
                ViewModel.GetAllChallengeRanking();
            }
            else
            {
                ViewModel.GetChallengeRanking(long.Parse(rankingChallengeID.Text));
            }
        }

        private void Get_User_Ranking(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(rankingUserID.Text))
            {
                ViewModel.GetUserRanking(long.Parse(rankingUserID.Text));
            }
        }

        private void TextBlock_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
