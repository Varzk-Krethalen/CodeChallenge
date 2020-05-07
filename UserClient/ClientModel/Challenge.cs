namespace ClientModels
{
    public enum Language { JAVA }

    public class Challenge : IChallenge
    {
        public Challenge(long challengeID, string name, Language language, string initialCode)
        {
            this.challengeID = challengeID;
            this.name = name;
            this.language = language;
            this.initialCode = initialCode;
        }

        public long challengeID { get; set; }

        public string name { get; set; }

        public Language language { get; set; }

        public string initialCode { get; set; }
    }
}