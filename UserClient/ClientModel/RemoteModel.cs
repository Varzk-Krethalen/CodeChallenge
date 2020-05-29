using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace ClientModels
{
    public partial class RemoteModel : IModel
    {
        private RestClient client;
        private string baseUri = "http://localhost:59876/";

        public RemoteModel()
        {
            client = new RestClient(baseUri);
        }

        public Challenge GetChallenge(long challengeId)
        {
            throw new NotImplementedException();
        }

        public ChallengeResult SubmitChallenge(long challengeId, string challengeCode)
        {
            ChallengeSubmission challengeSubmission = new ChallengeSubmission(challengeId, challengeCode);
            RestRequest request = new RestRequest("challenge/submit", Method.POST);
            request.AddParameter("application/json", JsonConvert.SerializeObject(challengeSubmission), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<ChallengeResult>(response.Content);
            }
            return new ChallengeResult() { ResultString= "Failed to validate with server", Success = false };
        }

        public List<Challenge> GetChallenges()
        {
            RestRequest request = new RestRequest("challenge/getAll");
            IRestResponse response = client.Get(request);
            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<List<Challenge>>(response.Content);
            }
            return new List<Challenge>();
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
