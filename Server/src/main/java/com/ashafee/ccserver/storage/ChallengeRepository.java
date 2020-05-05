package com.ashafee.ccserver.storage;

import com.ashafee.ccserver.challenge.Challenge;

import javax.transaction.Transactional;

@Transactional
public interface ChallengeRepository extends GenericRepository<Challenge> {

}

