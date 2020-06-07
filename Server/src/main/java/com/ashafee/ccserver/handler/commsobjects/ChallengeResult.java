package com.ashafee.ccserver.handler.commsobjects;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data @AllArgsConstructor
public class ChallengeResult {
    private String resultString;
    private boolean success;
}
