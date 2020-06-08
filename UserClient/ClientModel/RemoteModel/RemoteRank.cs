using ClientModels.Interfaces;
using ClientModels.RemoteModelObjects;
using Newtonsoft.Json;

namespace ClientModels
{
    public class RemoteRank : IRank
    {
        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("user"), JsonConverter(typeof(JsonConcreteConverter<IUser, RemoteUser>))]
        public IUser User { get; set; }

        [JsonProperty("challengesCompleted")]
        public int ChallengesCompleted { get; set; }
    }
}