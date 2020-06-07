using ClientModel.Interfaces;
using ClientModels.RemoteModelObjects;
using System.Collections.Generic;

namespace ClientModels.Interfaces
{
    public interface IModel
    {
        IUser GetUser(long userId);
        bool ValidateUser(RemoteUser user);
        bool AddUser(IUser user);
        bool UpdateUser(IUser user);
        bool DeleteUser(long userId);

        List<IRanking> GetRanking(long rankingId);

        List<IRanking> GetRankings();

        IChallenge GetChallenge(long challengeId);
        List<IChallenge> GetChallenges();
        IChallengeResult ValidateChallenge(long challengeId, string challengeCode);
        bool AddChallenge(IChallenge challenge);
        bool UpdateChallenge(IChallenge challenge);
        bool DeleteChallenge(long challengeId);


        IChallenge NewChallengeInstance();
        IRanking NewRankingInstance();
        ITest NewTestInstance();
        IChallengeResult NewChallengeResultInstance();
        IUser NewUserInstance();
    }
}
