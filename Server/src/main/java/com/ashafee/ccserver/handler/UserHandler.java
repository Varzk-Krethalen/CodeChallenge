package com.ashafee.ccserver.handler;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController()
@RequestMapping("/user")
public class UserHandler {

    @GetMapping("/greeting")
    public Test greeting(@RequestParam(value = "name", defaultValue = "World") String name) {
        return new Test();
    }
}

class Test {
    private int number;
    private String string;

    Test() {
        number = 111;
        string = "222";
    }

    public int getNumber() {
        return number;
    }

    public String getString() {
        return string;
    }
}
