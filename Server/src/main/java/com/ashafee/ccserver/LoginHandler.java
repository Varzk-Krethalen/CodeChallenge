package com.ashafee.ccserver;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController()
@RequestMapping("/login")
public class LoginHandler {

    @GetMapping("")
    public String login() {
        return "Login not yet implemented";
    }
}
