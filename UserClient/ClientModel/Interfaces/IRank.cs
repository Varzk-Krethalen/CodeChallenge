using ClientModel.Interfaces;

namespace ClientModels.Interfaces
{
    public interface IRank
    {
        int Rank { get; }
        IUser User { get; }
        int ChallengesCompleted { get; }
    }
}
