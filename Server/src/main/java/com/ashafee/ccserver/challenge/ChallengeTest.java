package com.ashafee.ccserver.challenge;

import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;

@Entity @Data @NoArgsConstructor
public class ChallengeTest {
    @Id @GeneratedValue(strategy= GenerationType.AUTO)
    private long testID;
    private String inputArgs;
    private String expectedOutput;
}
