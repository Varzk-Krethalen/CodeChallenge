using ClientModels.Interfaces;
using Newtonsoft.Json;

namespace ClientModels
{
    public class RemoteRank : IRank
    {
        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("objectID")]
        public long ObjectID { get; set; }

        [JsonProperty("objectName")]
        public string ObjectName { get; set; }

        [JsonProperty("challengesCompleted")]
        public int ChallengesCompleted { get; set; }
    }
}