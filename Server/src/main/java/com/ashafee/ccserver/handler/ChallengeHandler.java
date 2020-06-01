package com.ashafee.ccserver.handler;

import com.ashafee.ccserver.challenge.*;
import com.ashafee.ccserver.storage.JPADataAccessor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.*;

import javax.transaction.Transactional;
import java.util.List;
import java.util.Set;

@RestController @RequestMapping("/challenge") @Transactional
public class ChallengeHandler {

    @Autowired
    JPADataAccessor<Challenge> accessor;
    @Autowired
    JPADataAccessor<ChallengeTest> testAccessor;

    @GetMapping("/getAll")
    public List<Challenge> getChallenge() {
        return accessor.getAllEntities();
    }

    static class ChallengeSubmission {
        public String challengeCode;
        public long challengeId;
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

    @PostMapping(value = "/add")
    public boolean addChallenge(@RequestBody Challenge challenge) {
        challenge.setChallengeID(0);
        Challenge savedChallenge = SaveChallenge(challenge);
        return savedChallenge.getChallengeID() != 0;
    }

    //will also add if it doesn't exist
    @PostMapping(value = "/update")
    public boolean updateChallenge(@RequestBody Challenge challenge) {
        Challenge savedChallenge = SaveChallenge(challenge);
        return savedChallenge.getChallengeID() == challenge.getChallengeID();
    }

    private Challenge SaveChallenge(Challenge challenge) {
        //SaveTests(challenge.getTests());
        return accessor.saveEntity(challenge);
    }

    private void SaveTests(Set<ChallengeTest> tests) {
        for (ChallengeTest test : tests) {
            testAccessor.saveEntity(test);
        }
    }

    @DeleteMapping(value = "/delete")
    public boolean updateChallenge(@RequestParam long challengeId) {
        return accessor.deleteEntity(challengeId);
    }
}
