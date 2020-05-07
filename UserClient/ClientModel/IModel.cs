using System.Collections.Generic;

namespace ClientModels
{
    public interface IModel
    {
        Challenge GetChallenge(long challengeId);
        
        List<Challenge> GetChallenges();

        List<Ranking> GetRanking(long rankingId);

        List<Ranking> GetRankings();
    }
}
