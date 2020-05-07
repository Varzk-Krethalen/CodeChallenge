﻿using ClientModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ClientGUI
{
    public class MainViewModel : ViewModelBase
    {
        private string userName;
        private string challengeCode;
        private string challengeStatus;
        private IChallenge currentChallenge;

        private string UserData { get; set; }
        public string UserName { get => userName; set => SetProperty(ref userName, value); }
        public string ChallengeCode
        {
            get => challengeCode; set
            {
                SetProperty(ref challengeCode, value);
                CurrentChallenge.initialCode = challengeCode;
            }
        }
        public string ChallengeStatus { get => challengeStatus; set => SetProperty(ref challengeStatus, value); }
        public IModel Model { get; set; } = new RemoteModel();
        private IChallenge CurrentChallenge
        {
            get => currentChallenge; set
            {
                currentChallenge = value;
                ChallengeCode = currentChallenge.initialCode;
            }
        }

        public MainViewModel()
        {
            if (RequestLogin() == false)
            {
                //Close the window.
            }
        }

        public bool RequestLogin()
        {
            LoginWindow loginWindow = new LoginWindow();
            if (loginWindow.ShowDialog() == true)
            {
                UserData = loginWindow.UserData;
                UserName = $"Hello, {UserData.Split(',')[0]}";
                return true;
            }
            return false;
        }

        public void RetrieveChallenge()
        {
            ChallengeStatus = "updating...";
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler((sender, e) =>
            {
                List<Challenge> challenges = Model.GetChallenges();
                CurrentChallenge = challenges[0];
            });
            worker.RunWorkerAsync();
        }

        public void SubmitChallenge()
        {
            if (CurrentChallenge != null)
            {
                ChallengeStatus = "updating...";
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += new DoWorkEventHandler((sender, e) =>
                {
                    ChallengeStatus = Model.SubmitChallenge(CurrentChallenge).ResultString;
                }); //not using Success - remove?
                worker.RunWorkerAsync();
            }
        }
    }
}