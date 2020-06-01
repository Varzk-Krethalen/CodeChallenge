﻿using ClientModels.Interfaces;
using ClientModels.RemoteModelObjects;
using System.Collections.Generic;
using System.ComponentModel;

namespace ClientGUI
{
    public class MainViewModel : ViewModelBase
    {
        private string userName;
        private string userCode;
        private string challengeStatus;
        private List<IChallenge> challengeList;
        private IChallenge selectedChallenge;
        private string challengeDesc;
        private IChallenge currentChallenge;

        private string UserData { get; set; } //use a User object instead
        public string UserName { get => userName; set => SetProperty(ref userName, value); }
        public string UserCode { get => userCode; set => SetProperty(ref userCode, value); }
        public string ChallengeStatus { get => challengeStatus; set => SetProperty(ref challengeStatus, value); }
        public IModel Model { get; private set; } = new RemoteModel();//TODO: pass in from App
        public IChallenge CurrentChallenge { get => currentChallenge; private set => SetProperty(ref currentChallenge, value); }
        public bool AdminToolsEnabled { get; private set; }
        public List<IChallenge> ChallengeList { get => challengeList; private set => SetProperty(ref challengeList, value); }
        public IChallenge SelectedChallenge
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
            ChallengeList = new List<IChallenge>();
            CurrentChallenge = Model.NewChallengeInstance();
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

        public bool LoadSelectedChallenge()
        {
            if (SelectedChallenge != null)
            {
                ChallengeStatus = "loading...";
                CurrentChallenge = SelectedChallenge.GetCopy();
                UserCode = CurrentChallenge.initialCode;
                return true;
            }
            return false;
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
                    ChallengeStatus = Model.SubmitChallenge(CurrentChallenge.challengeID, UserCode).ResultString;
                }); //TODO: add proper completion dialog on success
                //TODO: consider changing to a submit bool, with getLastResult thingy
                worker.RunWorkerAsync();
            }
        }
    }
}
