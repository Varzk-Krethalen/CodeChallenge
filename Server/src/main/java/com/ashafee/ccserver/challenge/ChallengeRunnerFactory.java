package com.ashafee.ccserver.challenge;

import com.ashafee.ccserver.challenge.java.JavaChallengeRunner;

public class ChallengeRunnerFactory {
    public static ChallengeRunner getChallengeRunner(Language language) throws Exception {
        switch (language)
        {
            case JAVA:
                return new JavaChallengeRunner();
            default:
                throw new Exception("No challenge runner found for language: " + language.toString());
        }
    }
}
