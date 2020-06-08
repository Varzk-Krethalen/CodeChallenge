package com.ashafee.ccserver.ranking;

import com.ashafee.ccserver.user.User;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;
import java.util.List;

@Data @AllArgsConstructor
public class Ranking {
    private String rankingName;
    private List<Rank> ranks;
}

