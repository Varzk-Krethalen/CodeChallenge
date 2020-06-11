namespace ClientModels.Interfaces
{
    public interface IChallengeResult
    {
        string ResultString { get; set; }
        bool Success { get; set; }
    }
}