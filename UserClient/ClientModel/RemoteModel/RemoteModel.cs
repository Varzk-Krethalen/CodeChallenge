using ClientModels.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Management;

namespace ClientModels.RemoteModelObjects
{
    public partial class RemoteModel : IModel
    {
        public RestClient Client { get; set; }
        public string BaseUri { get; set; }

        public RemoteModel(string baseUri)
        {
            BaseUri = baseUri;
            NewClient();
        }

        private void NewClient()
        {
            Client = new RestClient(BaseUri);
        }

        private IRestResponse ExecuteBasicPOST(string uri, object objectToSend)
        {
            RestRequest request = new RestRequest(uri, Method.POST);
            if (objectToSend != null)
            {
                request.AddParameter("application/json", JsonConvert.SerializeObject(objectToSend), ParameterType.RequestBody);
            }
            return Client.Execute(request);
        }

        private IRestResponse ExecuteMultiParamPOST(string uri, List<Parameter> parameters)
        {
            RestRequest request = new RestRequest(uri, Method.POST);
            request.AddOrUpdateParameters(parameters);
            return Client.Execute(request);
        }

        private bool AddOrUpdate(string uri, List<Parameter> parameters)
        {
            IRestResponse response = ExecuteMultiParamPOST(uri, parameters);
            return response.IsSuccessful && bool.Parse(response.Content);
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


        public List<IUser> GetUsers()
        {
            RestRequest request = new RestRequest("user/getAll");
            IRestResponse response = Client.Get(request);
            if (response.IsSuccessful)
            {
                var x = JsonConvert.DeserializeObject<List<RemoteUser>>(response.Content);
                var y = x.Cast<IUser>();
                return y.ToList();
            }
            return new List<IUser>();
        }

        public bool ValidateUser(string username, string password, out IUser user)
        {
            user = null;
            Client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("auth/true");
            if (bool.TryParse(Client.Get(request).Content, out bool success))
            {
                if (success)
                {
                    if (GetUserByName(username, out IUser retrievedUser))
                    {
                        user = retrievedUser;
                        return true;
                    }
                }    
            }
            return false;
        }

        private bool GetUserByName(string username, out IUser user)
        {
            user = null;
            RestRequest request = new RestRequest("user/getByName");
            request.AddParameter(new Parameter("userName", username, ParameterType.GetOrPost));
            IRestResponse response = Client.Get(request);
            if (response.IsSuccessful)
            {
                user = JsonConvert.DeserializeObject<RemoteUser>(response.Content);
            }
            return response.IsSuccessful;
        }

        public void Logout()
        {
            ExecuteBasicPOST("auth/logout", null);
            NewClient();
        }

        public bool AddUser(IUser user, string newPass)
        {
            RemoteUser remoteUser = user.GetCopy() as RemoteUser;
            remoteUser.Password = newPass;
            return AddOrUpdate("user/add", remoteUser);
        }

        public bool UpdateUserName(long userId, string name) => AddOrUpdate("user/updateName", new List<Parameter>()
            {
                new Parameter("userId", userId, ParameterType.GetOrPost),
                new Parameter("userName", name, ParameterType.GetOrPost),
            });

        public bool UpdateUserPass(long userId, string pass) => AddOrUpdate("user/updatePass", new List<Parameter>()
            {
                new Parameter("userId", userId, ParameterType.GetOrPost),
                new Parameter("userPass", pass, ParameterType.GetOrPost),
            });

        public bool UpdateUserType(long userId, UserType userType) => AddOrUpdate("user/updatType", new List<Parameter>()
            {
                new Parameter("userId", userId, ParameterType.GetOrPost),
                new Parameter("userType", userType, ParameterType.GetOrPost),
            });

        public bool DeleteUser(long userId) => DeleteById(userId, "userId", "user/delete");


        public IRanking GetAllChallengeRanking()
        {
            RestRequest request = new RestRequest("ranking/allChallenges");
            return GetRankingResponse(request);
        }

        public IRanking GetRankingByChallenge(long challengeID)
        {
            RestRequest request = new RestRequest("ranking/challenge");
            request.AddParameter("challengeId", challengeID, ParameterType.GetOrPost);
            return GetRankingResponse(request);
        }

        public IRanking GetRankingByUser(long userID)
        {
            RestRequest request = new RestRequest("ranking/user");
            request.AddParameter("userId", userID, ParameterType.GetOrPost);
            return GetRankingResponse(request);
        }

        private IRanking GetRankingResponse(RestRequest request)
        {
            IRestResponse response = Client.Get(request);
            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<RemoteRanking>(response.Content);
            }
            return new RemoteRanking();
        }


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
