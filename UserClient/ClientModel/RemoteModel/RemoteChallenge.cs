using ClientModels.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ClientModels.RemoteModelObjects
{

    public class RemoteChallenge : IChallenge
    {
        [JsonProperty("challengeID")]
        public long ChallengeID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("language")]
        public Language Language { get; set; }

        [JsonProperty("initialCode")]
        public string InitialCode { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("tests", ItemConverterType = (typeof(JsonConcreteConverter<ITest, RemoteTest>)))]
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
            return new RemoteChallenge(ChallengeID, Name, Language, InitialCode, Description) { Tests = new List<ITest>(Tests) };
        }

    }
}