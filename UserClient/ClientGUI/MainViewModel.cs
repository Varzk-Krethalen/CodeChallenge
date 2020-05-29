using ClientModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace ClientGUI
{
    public class MainViewModel : ViewModelBase
    {
        private string userName;
        private string challengeCode;
        private string challengeStatus;
        private List<Challenge> challengeList;
        private Challenge selectedChallenge;
        private string challengeDesc;

        private string UserData { get; set; }
        public string UserName { get => userName; set => SetProperty(ref userName, value); }
        public string ChallengeCode { get => challengeCode; set => SetProperty(ref challengeCode, value); }
        public string ChallengeStatus { get => challengeStatus; set => SetProperty(ref challengeStatus, value); }
        public IModel Model { get; private set; } = new RemoteModel();//TODO: pass in from App
        private IChallenge CurrentChallenge { get; set; }
        public bool AdminToolsEnabled { get; private set; }
        public List<Challenge> ChallengeList { get => challengeList; private set => SetProperty(ref challengeList, value); }
        public Challenge SelectedChallenge
        {
            get => selectedChallenge; set
            {
                selectedChallenge = value;
                ChallengeDesc = $"{value.name}:\r\n\r\n{value.description}\r\n\r\nInitial Code:\r\n{value.initialCode}";
            }
        }
        public string ChallengeDesc { get => challengeDesc; private set => SetProperty(ref challengeDesc, value); }

        public MainViewModel()
        {
            ChallengeList = new List<Challenge>();
            CurrentChallenge = new Challenge();
            AdminToolsEnabled = true;
        }

        public bool RequestLogin()
        {
            LoginDialog loginWindow = new LoginDialog();
            if (loginWindow.ShowDialog() == true)
            {
                UserData = loginWindow.UserData;
                UserName = $"{UserData.Split(',')[0]}";
                return true; //TODO: proper validation via model
            }
            return false;
        }

        public void LoadSelectedChallenge()
        {
            ChallengeStatus = "loading...";
            CurrentChallenge = SelectedChallenge; //need a deep copy...
            ChallengeCode = CurrentChallenge.initialCode;
        }

        public void RefreshChallenges()
        {
            ChallengeDesc = "Refreshing...";
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler((sender, e) =>
            {
                ChallengeList = Model.GetChallenges();
                if (ChallengeList.Count == 0)
                {
                    ChallengeDesc = "Failed to communicate with server";
                }
            });
            worker.RunWorkerAsync();
        }

        internal void EditSelectedChallenge(IChallenge challenge)
        {
            ChallengeDesc = "Updating Challenge...";
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler((sender, e) =>
            {
                if (Model.UpdateChallenge(challenge))
                {
                    RefreshChallenges();
                }
                else
                {
                    ChallengeDesc = "Failed to communicate with server";
                }
            });
            worker.RunWorkerAsync();
        } //TODO: DRY

        internal void AddChallenge(IChallenge challenge)
        {
            ChallengeDesc = "Adding Challenge...";
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler((sender, e) =>
            {
                if (Model.AddChallenge(challenge))
                {
                    RefreshChallenges();
                }
                else
                {
                    ChallengeDesc = "Failed to communicate with server";
                }
            });
            worker.RunWorkerAsync();
        }

        internal void DeleteSelectedChallenge()
        {
            Model.DeleteChallenge(SelectedChallenge.challengeID);
            RefreshChallenges();
        }

        public void SubmitChallenge()
        {
            if (CurrentChallenge != null)
            {
                ChallengeStatus = "updating...";
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += new DoWorkEventHandler((sender, e) =>
                {
                    ChallengeStatus = Model.SubmitChallenge(CurrentChallenge.challengeID, ChallengeCode).ResultString;
                }); //TODO: add proper completion dialog on success
                //TODO: consider changing to a submit bool, with getLastResult thingy
                worker.RunWorkerAsync();
            }
        }
    }
}
