package com.ashafee.ccserver.ranking;

import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;

@Entity @Data @NoArgsConstructor
public class Ranking {
    @Id @GeneratedValue(strategy= GenerationType.AUTO)
    private long rankingID;         //move id/name/desc into a LeaderBoard table. This should be composite.
    private String rankingName;
    private String rankingDesc;
//    @ManyToOne @JoinColumn(name = "userID")
//    private User userID;
    private int userScore;
}

