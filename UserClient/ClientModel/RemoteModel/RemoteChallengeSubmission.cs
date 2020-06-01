using Newtonsoft.Json;

namespace ClientModels.RemoteModelObjects
{
    public class RemoteChallengeSubmission
    {
        [JsonProperty("challengeCode")]
        public string ChallengeCode { get; set; }

        [JsonProperty("challengeId")]
        public long ChallengeId { get; set; }

        public RemoteChallengeSubmission(long challengeId, string challengeCode)
        {
            ChallengeId = challengeId;
            ChallengeCode = challengeCode;
        }//TODO: Add user id, so server knows who it is
    }
}
