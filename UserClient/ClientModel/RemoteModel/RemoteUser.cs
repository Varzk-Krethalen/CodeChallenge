using ClientModel.Interfaces;
using Newtonsoft.Json;

namespace ClientModels.RemoteModelObjects
{
    public class RemoteUser : IUser
    {
        [JsonProperty("userID")]
        public long UserID { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("userType")]
        public UserType UserType { get; set; }

        public IUser GetCopy()
        {
            return new RemoteUser() { UserID = UserID, Username = Username, UserType = UserType };
        }
    }


}