package com.ashafee.ccserver.challenge.java;

import com.ashafee.ccserver.challenge.Challenge;
import com.ashafee.ccserver.challenge.ChallengeRunner;


public class JavaChallengeRunner implements ChallengeRunner {
    private String result;
    private String challengeBuildDir = "target/classes/challengebuilds";
    private String challengeClassName = "Challenge";

    @Override
    public Boolean challengeCodeValid(Challenge challenge, String code) {
        JavaCompiler compiler = new JavaCompiler(challengeBuildDir, challengeClassName);
        result = compiler.compile(code);
        result = compiler.getLastOutput();

        boolean success = true;
        return success ? true : false;
    }

    @Override
    public String lastOutput() {
        return result;
    }
}

