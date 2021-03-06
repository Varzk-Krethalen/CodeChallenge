package com.ashafee.ccserver.user;

import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

@Entity @Data @NoArgsConstructor
public class User {
    @Id @GeneratedValue(strategy= GenerationType.AUTO)
    private long userID;
    private String username;
    private String password;
    private UserType userType;
}
