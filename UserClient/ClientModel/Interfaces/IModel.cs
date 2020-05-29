﻿using System.Collections.Generic;

namespace ClientModels
{
    public interface IModel
    {
        Challenge GetChallenge(long challengeId);

        ChallengeResult SubmitChallenge(long challengeId, string challengeCode);

        List<Challenge> GetChallenges();

        List<Ranking> GetRanking(long rankingId);

        List<Ranking> GetRankings();
    }
}