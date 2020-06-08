using ClientModel.Interfaces;
using ClientModels.Interfaces;
using Newtonsoft.Json;

namespace ClientModels
{
    public class RemoteRank : IRank
    {
        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("user")]
        public IUser User { get; }

        [JsonProperty("challengesCompleted")]
        public int ChallengesCompleted { get; set; }
    }
}