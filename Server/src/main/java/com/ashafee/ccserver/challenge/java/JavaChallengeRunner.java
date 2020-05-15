package com.ashafee.ccserver.challenge.java;

import com.ashafee.ccserver.challenge.Challenge;
import com.ashafee.ccserver.challenge.ChallengeRunner;


public class JavaChallengeRunner implements ChallengeRunner {
    private String result;

    @Override
    public Boolean validateChallenge(Challenge challenge) {

        StringBuilder sb = new StringBuilder(64);
        sb.append("public class Challenge {\n");
        sb.append("    public static void main(String[] args) {\n");
        sb.append("        System.out.println(\"Hello world\");\n");
        sb.append("    }\n");
        sb.append("}\n");

        Compiler compiler = new Compiler();
        result = compiler.compile(sb.toString());
        result = compiler.getLastOutput();
        return true;
    }

    @Override
    public String getlastOutput() {
        return result;
    }
}

