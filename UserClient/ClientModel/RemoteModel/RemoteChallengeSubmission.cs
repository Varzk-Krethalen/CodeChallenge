namespace ClientModels.RemoteModelObjects
{
    public class RemoteChallengeSubmission
    {
        public string challengeCode { get; set; }
        public long challengeId { get; set; }

        public RemoteChallengeSubmission(long challengeId, string challengeCode)
        {
            this.challengeId = challengeId;
            this.challengeCode = challengeCode;
        }
    }
}
