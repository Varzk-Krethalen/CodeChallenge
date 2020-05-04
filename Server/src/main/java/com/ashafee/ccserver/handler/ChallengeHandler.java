package com.ashafee.ccserver.handler;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/challenge")
public class ChallengeHandler {

    @GetMapping("")
    public String getChallenge() {
        return "Challenge access not yet implemented";
    }
}
