package com.ashafee.ccserver.challenge;

import com.fasterxml.jackson.databind.annotation.JsonSerialize;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;
import java.util.HashSet;
import java.util.Set;

@Entity @Data @NoArgsConstructor
public class Challenge {
    @Id @GeneratedValue(strategy= GenerationType.AUTO)
    private long challengeID;
    private String name;
    private Language language;
    private String initialCode;
    private String description;
    @OneToMany(fetch = FetchType.LAZY)
    @JoinColumn(name = "challengeID") //extend by instead referencing a list of challenge-test from another table
    private Set<ChallengeTest> tests = new HashSet<>(); //that is, challenge independent tests
}   //Extension: last modified date, author
