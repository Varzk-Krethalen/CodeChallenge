namespace ClientModels.Interfaces
{
    public interface IRank
    {
        int Rank { get; }
        long ObjectID { get; }
        string ObjectName { get; }
        int ChallengesCompleted { get; }
    }
}
