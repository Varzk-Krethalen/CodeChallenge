namespace ClientModels
{
    public interface IChallenge
    {
        long challengeID { get; set; }
        string initialCode { get; set; }
        Language language { get; set; }
        string name { get; set; }
    }
}