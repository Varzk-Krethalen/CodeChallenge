package com.ashafee.ccserver.handler;

import com.ashafee.ccserver.challenge.Challenge;
import com.ashafee.ccserver.challenge.Language;
import com.ashafee.ccserver.storage.JPADataAccessor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/user")
public class UserHandler {

    @Autowired
    JPADataAccessor accessor;

    @GetMapping("/greeting")
    public Iterable<Challenge> greeting(@RequestParam(value = "name", defaultValue = "World") String name) {
        Challenge challenge = new Challenge();
        challenge.setChallengeID(1);
        challenge.setName("name");
        challenge.setLanguage(Language.JAVA);
        challenge.setInitialCode("throw new exception(blah)");
        accessor.saveChallenge(challenge);
        challenge.setName("aha!");
        accessor.saveChallenge(challenge);
        return null;
    }
}