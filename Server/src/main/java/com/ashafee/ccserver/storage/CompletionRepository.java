package com.ashafee.ccserver.storage;

import com.ashafee.ccserver.challenge.Challenge;
import com.ashafee.ccserver.challenge.ChallengeCompletion;

import javax.transaction.Transactional;

@Transactional
public interface CompletionRepository extends GenericRepository<ChallengeCompletion> {
    int countByUserID(long userID);
    int countByUserIDAndChallengeID(long userID, long challengeID);
}

