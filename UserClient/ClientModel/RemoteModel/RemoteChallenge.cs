using ClientModels.Interfaces;
using System.Collections.Generic;

namespace ClientModels.RemoteModelObjects
{

    public class RemoteChallenge : IChallenge
    {
        public long challengeID { get; set; }

        public string name { get; set; }

        public Language language { get; set; }

        public string initialCode { get; set; }

        public string description { get; set; }
        public List<ITest> tests { get; set; } = new List<ITest>();

        public RemoteChallenge() { }

        public RemoteChallenge(long challengeID, string name, Language language, string initialCode, string description)
        {
            this.challengeID = challengeID;
            this.name = name;
            this.language = language;
            this.initialCode = initialCode;
            this.description = description;
        }

        public IChallenge GetCopy()
        {
            return new RemoteChallenge(challengeID, name, language, initialCode, description);
        }

    }
}