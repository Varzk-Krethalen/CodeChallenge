namespace ClientModels
{
    public interface IChallenge
    {
        long challengeID { get; set; }
        string InitialCode { get; set; }
        Language language { get; set; }
        string name { get; set; }
    }
}