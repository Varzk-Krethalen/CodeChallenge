using ClientModels.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ClientModels.RemoteModelObjects
{

    public class RemoteChallenge : IChallenge
    {
        [JsonProperty(PropertyName = "challengeID")]
        public long ChallengeID { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "language")]
        public Language Language { get; set; }

        [JsonProperty(PropertyName = "initialCode")]
        public string InitialCode { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "tests")]
        public List<ITest> Tests { get; set; } = new List<ITest>();

        public RemoteChallenge() { }

        public RemoteChallenge(long challengeID, string name, Language language, string initialCode, string description)
        {
            ChallengeID = challengeID;
            Name = name;
            Language = language;
            InitialCode = initialCode;
            Description = description;
        }

        public IChallenge GetCopy()
        {
            return new RemoteChallenge(ChallengeID, Name, Language, InitialCode, Description);
        }

    }
}