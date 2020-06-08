package com.ashafee.ccserver.ranking;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;

@Data @AllArgsConstructor
public class Ranking {
    private long rankingID;         //move id/name/desc into a LeaderBoard table. This should be composite.
    private String rankingName;
    private String rankingDesc;
//    @ManyToOne @JoinColumn(name = "userID")
//    private User userID;
    private int userScore;
}
//TODO: Replace with a calculation
