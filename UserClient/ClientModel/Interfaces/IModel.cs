using ClientModel.Interfaces;
using System.Collections.Generic;

namespace ClientModels.Interfaces
{
    public interface IModel
    {
        IChallenge GetChallenge(long challengeId);

        IChallengeResult SubmitChallenge(long challengeId, string challengeCode);

        List<IChallenge> GetChallenges();

        List<IRanking> GetRanking(long rankingId);

        List<IRanking> GetRankings();
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
