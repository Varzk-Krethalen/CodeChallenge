package com.ashafee.ccserver.storage;

import com.ashafee.ccserver.challenge.Challenge;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.Optional;

@Service
public class JPADataAccessor {
    @Autowired
    private ChallengeRepository dataRepository;

    public Iterable<Challenge> getAllChallenges() {
        return dataRepository.findAll();
    }

    public Optional<Challenge> getChallenge(int challengeID) {
        return dataRepository.findById(challengeID);
    }

    public void saveChallenge(Challenge challenge) {
        dataRepository.save(challenge);
    }

} //pull into abstract, implement as challengeaccessor etc?
// use as T so not just challenge?
