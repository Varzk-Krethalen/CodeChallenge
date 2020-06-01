using ClientModels.Interfaces;

namespace ClientModels.RemoteModelObjects
{
    public class RemoteChallengeResult : IChallengeResult
    {
        public string ResultString { get; set; }
        public bool Success { get; set; }
    }
}