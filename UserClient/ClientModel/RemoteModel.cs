using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ClientModels
{
    public class RemoteModel : IModel
    {
        private RestClient client;
        private string baseUri = "http://localhost:59876/";

        public RemoteModel()
        {
            client = new RestClient(baseUri);
        }

        public List<Challenge> GetChallenges()
        {
            RestRequest request = new RestRequest("challenge/getAll");
            IRestResponse response = client.Get(request);
            Thread.Sleep(5000);
            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<List<Challenge>>(response.Content);
            }
            return new List<Challenge>();
        }

        public Challenge GetChallenge(long challengeId)
        {
            throw new NotImplementedException();
        }

        public User GetUser()
        {
            throw new NotImplementedException();
        }

        public bool ValidateUser(User user)
        {
            throw new NotImplementedException();
        }

        public List<Ranking> GetRankings()
        {
            throw new NotImplementedException();
        }

        public List<Ranking> GetRanking(long rankingId)
        {
            throw new NotImplementedException();
        }

    }
}
