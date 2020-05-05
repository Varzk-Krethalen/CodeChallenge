package com.ashafee.ccserver.challenge;

import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

@Entity @Data @NoArgsConstructor
public class Challenge {
    @Id @GeneratedValue(strategy= GenerationType.AUTO)
    private long challengeID;
    private String name;
    private Language language;
    private String initialCode;
//    private Date modifiedDate;
//    private String author;

}
