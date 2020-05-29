package com.ashafee.ccserver.handler;

import com.ashafee.ccserver.challenge.Challenge;
import com.ashafee.ccserver.storage.JPADataAccessor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import javax.transaction.Transactional;

@RestController @RequestMapping("/user") @Transactional
public class UserHandler {

    @Autowired
    JPADataAccessor<Challenge> accessor;

    @GetMapping("/greeting")
    public Iterable<Challenge> greeting(@RequestParam(value = "name", defaultValue = "World") String name) {
//        Challenge challenge = new Challenge();
//        challenge.setChallengeID(1);
//        challenge.setName("name");
//        challenge.setLanguage(Language.JAVA);
//        challenge.setInitialCode("throw new exception(blah)");
//        accessor.saveEntity(challenge);
//        var c = accessor.getAllEntities().get(0);
//        c.setName("four!");
        //accessor.saveEntity(challenge);
        return null;
    }
}