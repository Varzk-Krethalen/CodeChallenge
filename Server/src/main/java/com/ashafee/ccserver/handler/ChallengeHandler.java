package com.ashafee.ccserver.handler;

import com.ashafee.ccserver.challenge.Challenge;
import com.ashafee.ccserver.storage.JPADataAccessor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import javax.transaction.Transactional;
import java.util.List;

@RestController @RequestMapping("/challenge") @Transactional
public class ChallengeHandler {

    @Autowired
    JPADataAccessor<Challenge> accessor;

    @GetMapping("/getAll")
    public List<Challenge> getChallenge() {
        return accessor.getAllEntities();
    }
}
