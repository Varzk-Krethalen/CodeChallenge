package com.ashafee.ccserver.ranking;

import com.ashafee.ccserver.user.User;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data @NoArgsConstructor
public class Rank {
    private int rank;
    private long objectID;
    private String objectName;
    private Integer challengesCompleted;
}