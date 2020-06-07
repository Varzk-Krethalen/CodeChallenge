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
        public RestClient Client { get; set; }
        public string BaseUri { get; set; }

        public RemoteModel(string baseUri)
        {
            BaseUri = baseUri;
            Client = new RestClient(baseUri);
        }

        private IRestResponse ExecuteBasicPOST(string uri, object objectToSend)
        {
            RestRequest request = new RestRequest(uri, Method.POST);
            request.AddParameter("application/json", JsonConvert.SerializeObject(objectToSend), ParameterType.RequestBody);
            IRestResponse response = Client.Execute(request);
            return response;
        }

        private bool AddOrUpdate(string uri, object objectToSend)
        {
            IRestResponse response = ExecuteBasicPOST(uri, objectToSend);
            return response.IsSuccessful && bool.Parse(response.Content);
        }

        private bool DeleteById(long id, string idName, string uri)
        {
            RestRequest request = new RestRequest(uri, Method.DELETE);
            request.AddParameter(idName, id);
            IRestResponse response = Client.Execute(request);
            return response.IsSuccessful && bool.Parse(response.Content);
        }


        public IUser GetUser(long userId) //needed?
        {
            throw new NotImplementedException();
        }

        public bool ValidateUser(RemoteUser user)
        {
            throw new NotImplementedException();
        }

        public bool AddUser(IUser user) => AddOrUpdate("user/add", user);

        public bool UpdateUser(IUser user) => AddOrUpdate("user/update", user);

        public bool DeleteUser(long userId) => DeleteById(userId, "userId", "user/delete");


        public List<IRanking> GetRanking(long rankingId)
        {
            throw new NotImplementedException();
        }

        public List<IRanking> GetRankings()
        {
            throw new NotImplementedException();
        }


        public IChallenge GetChallenge(long challengeId)
        {
            throw new NotImplementedException();
        } //TODO: should need, as the list should be paged instead

        public List<IChallenge> GetChallenges()
        {
            RestRequest request = new RestRequest("challenge/getAll");
            IRestResponse response = Client.Get(request);
            if (response.IsSuccessful)
            {
                var x = JsonConvert.DeserializeObject<List<RemoteChallenge>>(response.Content);
                var y = x.Cast<IChallenge>();
                return y.ToList();
            }
            return new List<IChallenge>();
        }

        public IChallengeResult ValidateChallenge(long challengeId, string challengeCode)
        {
            RemoteChallengeSubmission challengeSubmission = new RemoteChallengeSubmission(challengeId, challengeCode);
            RestRequest request = new RestRequest("challenge/submit", Method.POST);
            request.AddParameter("application/json", JsonConvert.SerializeObject(challengeSubmission), ParameterType.RequestBody);

            IRestResponse response = Client.Execute(request);
            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<RemoteChallengeResult>(response.Content) as IChallengeResult;
            }
            return new RemoteChallengeResult() { ResultString = "Failed to validate with server", Success = false } as IChallengeResult;
        }

        public bool AddChallenge(IChallenge challenge) => AddOrUpdate("challenge/add", challenge);
        public bool UpdateChallenge(IChallenge challenge) => AddOrUpdate("challenge/update", challenge);
        public bool DeleteChallenge(long challengeId) => DeleteById(challengeId, "challengeId", "challenge/delete");

        public IChallenge NewChallengeInstance() => new RemoteChallenge();
        public IRanking NewRankingInstance() => new RemoteRanking();
        public ITest NewTestInstance() => new RemoteTest();
        public IChallengeResult NewChallengeResultInstance() => new RemoteChallengeResult();
        public IUser NewUserInstance() => new RemoteUser();
    }
}
