package com.ashafee.ccserver.challenge;

public interface ChallengeRunner {
    Boolean challengeCodeValid(Challenge challenge, String code);
    String lastOutput();
}
