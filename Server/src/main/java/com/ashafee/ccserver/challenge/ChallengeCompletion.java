package com.ashafee.ccserver.challenge;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;
import java.util.Date;

@Entity @Data @NoArgsConstructor
public class ChallengeCompletion {
    @Id
    @GeneratedValue(strategy= GenerationType.AUTO)
    private long resultID;
    private long challengeID;
    private long userID;
    private String code;
    @Temporal(TemporalType.TIMESTAMP)
    private Date completionTimeStamp;
}