package com.ashafee.ccserver.challenge.java;

import com.ashafee.ccserver.challenge.Challenge;
import com.ashafee.ccserver.challenge.ChallengeRunner;


public class JavaChallengeRunner implements ChallengeRunner {
    private String output;
    private String challengeBuildDir = "target/classes/challengebuilds";
    private String challengeClassName = "Challenge";

    @Override
    public Boolean challengeCodeValid(Challenge challenge, String code) {
        JavaCompiler compiler = new JavaCompiler(challengeBuildDir, challengeClassName);
        Boolean result = false;
        if (compiler.compile(code)) {
            result = compiler.runTests(challenge.getTests());
        }
        output = compiler.getLastOutput();
        return result;
    }

    @Override
    public String lastOutput() {
        return output;
    }
}

