using ClientModels;
using ClientModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ClientGUI
{
    public class MainViewModel : ViewModelBase
    {
        private const string modelCommsFailure = "Failed to communicate with server";
        private IUser user;
        private string userCode;
        private string challengeStatus;
        private List<IChallenge> challengeList;
        private List<IUser> userList;
        private IChallenge selectedChallenge;
        private string challengeDesc;
        private string userState;
        private IChallenge currentChallenge;
        private IRanking rankings;
        private bool adminToolsEnabled;

        public IUser User { get => user; set => SetProperty(ref user, value); }
        public string UserCode { get => userCode; set => SetProperty(ref userCode, value); }
        public string ChallengeStatus { get => challengeStatus; set => SetProperty(ref challengeStatus, value); }
        public IModel Model { get; private set; }
        public IChallenge CurrentChallenge { get => currentChallenge; private set => SetProperty(ref currentChallenge, value); }
        public bool AdminToolsEnabled { get => adminToolsEnabled; private set => SetProperty(ref adminToolsEnabled, value); }
        public List<IChallenge> ChallengeList { get => challengeList; private set => SetProperty(ref challengeList, value); }
        public List<IUser> UserList { get => userList; private set => SetProperty(ref userList, value); }
        public IChallenge SelectedChallenge
        {
            get => selectedChallenge; set
            {
                selectedChallenge = value;
                ChallengeDesc = $"{value.Name}:\r\n\r\n{value.Description}\r\n\r\nInitial Code:\r\n{value.InitialCode}";
            }
        }
        public IUser SelectedUser { get; set; }
        public string ChallengeDesc { get => challengeDesc; private set => SetProperty(ref challengeDesc, value); }
        public string UserState { get => userState; private set => SetProperty(ref userState, value); }
        public IRanking Rankings { get => rankings; set => SetProperty(ref rankings, value); }

        public MainViewModel(IModel model) => Model = model;

        public bool RequestLogin()
        {
            LoginDialog loginWindow = new LoginDialog(Model);
            if (loginWindow.ShowDialog() == true)
            {
                User = loginWindow.User;
                AdminToolsEnabled = User.UserType == UserType.ADMIN;
                RefreshChallenges();
                RefreshUsers();
                GetAllChallengeRanking();
                return true;
            }
            return false;
        }

        public void Logout() => Model.Logout();

        public bool LoadSelectedChallenge()
        {
            if (SelectedChallenge != null)
            {
                CurrentChallenge = SelectedChallenge.GetCopy();
                UserCode = CurrentChallenge.InitialCode;
                return true;
            }
            return false;
        }

        public void RefreshUsers()
        {
            UserState = "Refreshing...";
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler((sender, e) =>
            {
                UserList = Model.GetUsers();
                UserState = (UserList.Count == 0) ? modelCommsFailure : "";
            });
            worker.RunWorkerAsync();
        }

        internal void AddUser(IUser user, string newPass)
        {
            UserState = "Adding User...";
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler((sender, e) =>
            {
                if (Model.AddUser(user, newPass))
                {
                    RefreshUsers();
                }
                else
                {
                    UserState = modelCommsFailure;
                }
            });
            worker.RunWorkerAsync();
        }

        internal void SaveUserName(long userId, string username)
        {
            UserState = "Updating...";
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler((sender, e) =>
            {
                if (Model.UpdateUserName(userId, username))
                {
                    RefreshUsers();
                }
                else
                {
                    UserState = modelCommsFailure;
                }
            });
            worker.RunWorkerAsync();
        }

        internal void SaveUserPass(long userId, string newPass)
        {
            UserState = "Updating...";
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler((sender, e) =>
            {
                if (Model.UpdateUserPass(userId, newPass))
                {
                    RefreshUsers();
                }
                else
                {
                    UserState = modelCommsFailure;
                }
            });
            worker.RunWorkerAsync();
        }

        internal void SaveUserType(long userId, UserType userType)
        {
            UserState = "Updating...";
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler((sender, e) =>
            {
                Model.UpdateUserType(userId, userType);
                RefreshUsers();
            });
            worker.RunWorkerAsync();
        }

        internal void DeleteSelectedUser()
        {
            Model.DeleteUser(SelectedUser.UserID);
            RefreshUsers();
        }

        public void RefreshChallenges()
        {
            ChallengeDesc = "Refreshing...";
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler((sender, e) =>
            {
                ChallengeList = Model.GetChallenges();
                ChallengeDesc = (ChallengeList.Count == 0) ? modelCommsFailure : "";
            });
            worker.RunWorkerAsync();
        }

        internal void EditSelectedChallenge(IChallenge challenge)
        {
            ChallengeDesc = "Updating Challenge...";
            RunChallengeUpdateWorker(challenge, Model.UpdateChallenge);
        }

        internal void AddChallenge(IChallenge challenge)
        {
            ChallengeDesc = "Adding Challenge...";
            RunChallengeUpdateWorker(challenge, Model.AddChallenge);
        }

        private void RunChallengeUpdateWorker(IChallenge challenge, Func<IChallenge, bool> challengeTask)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler((sender, e) =>
            {
                if (challengeTask(challenge))
                {
                    RefreshChallenges();
                }
                else
                {
                    ChallengeDesc = modelCommsFailure;
                }
            });
            worker.RunWorkerAsync();
        }

        internal void DeleteSelectedChallenge()
        {
            Model.DeleteChallenge(SelectedChallenge.ChallengeID);
            RefreshChallenges();
        }

        public void SubmitChallenge(Action<bool> onComplete)
        {
            if (CurrentChallenge != null)
            {
                ChallengeStatus = "updating...";
                BackgroundWorker worker = new BackgroundWorker();
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler((sender, e) => onComplete((bool)e.Result));
                worker.DoWork += new DoWorkEventHandler(delegate (object sender, DoWorkEventArgs e)
                {
                    IChallengeResult challengeResult = Model.ValidateChallenge(CurrentChallenge.ChallengeID, UserCode);
                    ChallengeStatus = challengeResult.ResultString;
                    e.Result = challengeResult.Success;
                });
                worker.RunWorkerAsync();
            }
        }

        public void GetAllChallengeRanking()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler((sender, e) =>
            {
                Rankings = Model.GetAllChallengeRanking();
            });
            worker.RunWorkerAsync();
        }

        public void GetChallengeRanking(long challengeID)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler((sender, e) =>
            {
                Rankings = Model.GetRankingByChallenge(challengeID);
            });
            worker.RunWorkerAsync();
        }

        public void GetUserRanking(long userID)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler((sender, e) =>
            {
                Rankings = Model.GetRankingByUser(userID);
            });
            worker.RunWorkerAsync();
        }
    }
}
