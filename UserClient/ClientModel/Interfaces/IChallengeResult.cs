namespace ClientModels
{
    public interface IChallengeResult
    {
        string ResultString { get; set; }
        bool Success { get; set; }
    }
}