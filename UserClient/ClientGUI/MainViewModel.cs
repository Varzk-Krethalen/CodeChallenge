using ClientModels;
using System.Collections.Generic;
using System.ComponentModel;

namespace ClientGUI
{
    public class MainViewModel : ViewModelBase
    {
        private string userName;
        private string challengeCode;
        private string challengeStatus;

        private string UserData { get; set; }
        public string UserName { get => userName; set => SetProperty(ref userName, value); }
        public string ChallengeCode { get => challengeCode; set => SetProperty(ref challengeCode, value); }
        public string ChallengeStatus { get => challengeStatus; set => SetProperty(ref challengeStatus, value); }
        public IModel Model { get; set; } = new RemoteModel();
        private IChallenge CurrentChallenge { get; set; }
        public bool AdminToolsEnabled { get; private set; }

        public MainViewModel()
        {
            CurrentChallenge = new Challenge();
            AdminToolsEnabled = true;
        }

        public bool RequestLogin()
        {
            LoginWindow loginWindow = new LoginWindow();
            if (loginWindow.ShowDialog() == true)
            {
                UserData = loginWindow.UserData;
                UserName = $"{UserData.Split(',')[0]}";
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
                if (challenges.Count > 0)
                {
                    CurrentChallenge = challenges[0];
                    ChallengeCode = CurrentChallenge.initialCode;
                }
                else
                {
                    ChallengeStatus = "Failed to communicate with server";
                }
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
                    ChallengeStatus = Model.SubmitChallenge(CurrentChallenge.challengeID, ChallengeCode).ResultString;
                }); //TODO: add proper completion dialog on success
                //TODO: consider changing to a submit bool, with getLastResult thingy
                worker.RunWorkerAsync();
            }
        }
    }
}
