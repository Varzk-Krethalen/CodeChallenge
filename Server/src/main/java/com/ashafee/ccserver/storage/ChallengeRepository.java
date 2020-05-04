package com.ashafee.ccserver.storage;

import com.ashafee.ccserver.challenge.Challenge;
import org.springframework.data.repository.CrudRepository;

// This will be AUTO IMPLEMENTED by Spring into a Bean called userRepository
// CRUD refers Create, Read, Update, Delete

public interface ChallengeRepository extends CrudRepository<Challenge, Integer> {

}