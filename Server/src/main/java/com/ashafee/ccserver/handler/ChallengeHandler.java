package com.ashafee.ccserver.handler;

import com.ashafee.ccserver.challenge.Challenge;
import com.ashafee.ccserver.challenge.ChallengeResult;
import com.ashafee.ccserver.challenge.ChallengeRunner;
import com.ashafee.ccserver.challenge.ChallengeRunnerFactory;
import com.ashafee.ccserver.storage.JPADataAccessor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.*;

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

    @PostMapping(value = "/submit", consumes = MediaType.APPLICATION_JSON_VALUE)
    public ChallengeResult submitChallenge(@RequestBody Challenge challenge) {
        Boolean result = false;
        String resultString;
        try {
            ChallengeRunner runner = ChallengeRunnerFactory.getChallengeRunner(challenge.getLanguage());
            if (runner.validateChallenge(challenge) == true) {
                result = true;
            }
            resultString = runner.getlastOutput();
        } catch (Exception e) {
            resultString = e.getMessage();
        }
        return new ChallengeResult(resultString, result);
    }
}
