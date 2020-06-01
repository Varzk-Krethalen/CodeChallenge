using ClientModel.Interfaces;
using ClientModels.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientModels.RemoteModelObjects
{
    public partial class RemoteModel : IModel
    {
        private RestClient client;
        private string baseUri = "http://localhost:59876/";

        public RemoteModel()
        {
            client = new RestClient(baseUri);
        }

        public IChallenge GetChallenge(long challengeId)
        {
            throw new NotImplementedException();
        } //TODO: should need, as the list should be paged instead

        public IChallengeResult SubmitChallenge(long challengeId, string challengeCode)
        {
            RemoteChallengeSubmission challengeSubmission = new RemoteChallengeSubmission(challengeId, challengeCode);
            RestRequest request = new RestRequest("challenge/submit", Method.POST);
            request.AddParameter("application/json", JsonConvert.SerializeObject(challengeSubmission), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<RemoteChallengeResult>(response.Content) as IChallengeResult;
            }
            return new RemoteChallengeResult() { ResultString = "Failed to validate with server", Success = false } as IChallengeResult;
        }

        public List<IChallenge> GetChallenges()
        {
            RestRequest request = new RestRequest("challenge/getAll");
            IRestResponse response = client.Get(request);
            if (response.IsSuccessful)
            {
                var x = JsonConvert.DeserializeObject<List<RemoteChallenge>>(response.Content);
                var y = x.Cast<IChallenge>();
                return y.ToList();
            }
            return new List<IChallenge>();
        }

        public RemoteUser GetUser()
        {
            throw new NotImplementedException();
        }

        public bool ValidateUser(RemoteUser user)
        {
            throw new NotImplementedException();
        }

        public List<IRanking> GetRankings()
        {
            throw new NotImplementedException();
        }

        public List<IRanking> GetRanking(long rankingId)
        {
            throw new NotImplementedException();
        }

        public bool AddChallenge(IChallenge challenge)
        {
            RestRequest request = new RestRequest("challenge/add", Method.POST);
            request.AddParameter("application/json", JsonConvert.SerializeObject(challenge), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response.IsSuccessful && bool.Parse(response.Content);
        }

        public bool UpdateChallenge(IChallenge challenge)
        {
            RestRequest request = new RestRequest("challenge/update", Method.POST);
            request.AddParameter("application/json", JsonConvert.SerializeObject(challenge), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response.IsSuccessful && bool.Parse(response.Content);
        }

        public bool DeleteChallenge(long challengeId)
        {
            RestRequest request = new RestRequest("challenge/delete", Method.DELETE);
            request.AddParameter("challengeId", challengeId);
            IRestResponse response = client.Execute(request);
            return response.IsSuccessful && bool.Parse(response.Content);
        }




        public IChallenge NewChallengeInstance() => new RemoteChallenge();
        public IRanking NewRankingInstance() => new RemoteRanking();
        public ITest NewTestInstance() => new RemoteTest();
        public IChallengeResult NewChallengeResultInstance() => new RemoteChallengeResult();
        public IUser NewUserInstance() => new RemoteUser();
    }
}
