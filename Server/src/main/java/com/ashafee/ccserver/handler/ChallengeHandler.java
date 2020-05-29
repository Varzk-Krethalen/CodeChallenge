package com.ashafee.ccserver.handler;

import com.ashafee.ccserver.challenge.*;
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

    static class ChallengeSubmission {
        public long challengeId;
        public String challengeCode;
    }

    @PostMapping(value = "/submit", consumes = MediaType.APPLICATION_JSON_VALUE)
    public ChallengeResult submitChallenge(@RequestBody ChallengeSubmission challengeSubmission) {
        boolean result = false;
        String resultString;
        try {
            Challenge challenge = accessor.getEntity(challengeSubmission.challengeId)
                    .orElseThrow(() -> new Exception("Challenge " + challengeSubmission.challengeId + " not found"));
            ChallengeRunner runner = ChallengeRunnerFactory.getChallengeRunner(challenge.getLanguage());

            if (runner.challengeCodeValid(challenge, challengeSubmission.challengeCode)) {
                result = true;
            }
            resultString = runner.lastOutput();
        } catch (Exception e) {
            resultString = e.getMessage();
        }
        return new ChallengeResult(resultString, result);
    }

    //also return Id?
    @PostMapping(value = "/")
    public boolean addChallenge(@RequestParam String name, @RequestParam Language language, @RequestParam String initialCode) {
        return false; //TODO
    }
}
