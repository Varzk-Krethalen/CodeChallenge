package com.ashafee.ccserver.challenge;

public interface ChallengeRunner {
    Boolean validateChallenge(Challenge challenge);
    String getlastOutput();
}
