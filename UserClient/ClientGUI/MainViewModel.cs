using ClientModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ClientGUI
{
    public class MainViewModel : ViewModelBase
    {
        private string userName;
        private string userExample;
        private string challengeExample;
        private string rankingExample;

        private string UserData { get; set; }
        public string UserName { get => userName; set => SetProperty(ref userName, value); }
        public string UserExample { get => userExample; set => SetProperty(ref userExample, value); }
        public string ChallengeExample { get => challengeExample; set => SetProperty(ref challengeExample, value); }
        public string RankingExample { get => rankingExample; set => SetProperty(ref rankingExample, value); }
        public IModel Model { get; set; } = new RemoteModel();

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

        public void RetrieveExamples()
        {
            UserExample = "Example user!";
            RetrieveChallenges();
            RankingExample = "Example ranking!";
        }

        private void RetrieveChallenges()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler((sender, e) => {
                List<Challenge> challenges = Model.GetChallenges();
                ChallengeExample = challenges[0].InitialCode;
            });
            worker.RunWorkerAsync();
        }
    }
}
