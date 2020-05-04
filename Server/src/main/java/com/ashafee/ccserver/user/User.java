package com.ashafee.ccserver.user;

import com.ashafee.ccserver.challenge.Language;
import lombok.Data;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

@Entity
@Data
public class User {
    @Id
    @GeneratedValue(strategy= GenerationType.AUTO)
    private final Integer userID;
    private String username;
    private String password;
    private UserType userType;
//    private Ranking ranking? or int?
}
