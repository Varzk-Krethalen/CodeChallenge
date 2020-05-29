namespace ClientModels
{
    public enum Language { JAVA }

    public class Challenge : IChallenge
    {
        public Challenge() { }

        public Challenge(long challengeID, string name, Language language, string initialCode, string description)
        {
            this.challengeID = challengeID;
            this.name = name;
            this.language = language;
            this.initialCode = initialCode;
            this.description = description;
        }

        public long challengeID { get; set; }

        public string name { get; set; }

        public Language language { get; set; }

        public string initialCode { get; set; }

        public string description { get; set; }
    }
}