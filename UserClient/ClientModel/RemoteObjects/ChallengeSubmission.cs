namespace ClientModels
{
    public partial class RemoteModel
    {
        private class ChallengeSubmission
        {
            public string ChallengeCode { get; set; }
            public long ChallengeId { get; set; }

            public ChallengeSubmission(long challengeId, string challengeCode)
            {
                ChallengeId = challengeId;
                ChallengeCode = challengeCode;
            }
        }
    }
}
