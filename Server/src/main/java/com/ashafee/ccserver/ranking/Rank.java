package com.ashafee.ccserver.ranking;

import com.ashafee.ccserver.user.User;
import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class Rank {
    private int rank;
    private User user;
    private int challengesCompleted;
}
