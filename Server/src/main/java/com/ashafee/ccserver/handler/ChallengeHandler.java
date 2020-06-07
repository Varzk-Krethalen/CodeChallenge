package com.ashafee.ccserver.handler;

import com.ashafee.ccserver.challenge.*;
import com.ashafee.ccserver.handler.commsobjects.ChallengeResult;
import com.ashafee.ccserver.handler.commsobjects.ChallengeSubmission;
import com.ashafee.ccserver.storage.JPADataAccessor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.*;

import javax.transaction.Transactional;
import java.time.LocalDateTime;
import java.util.Date;
import java.util.List;

@RestController @RequestMapping("/challenge") @Transactional
public class ChallengeHandler {
    @Autowired
    JPADataAccessor<Challenge> accessor;
    @Autowired
    JPADataAccessor<ChallengeCompletion> completionAccessor;

    @GetMapping("/getAll")
    public List<Challenge> getChallenge() {
        return accessor.getAllEntities();
    }

    @PostMapping(value = "/submit", consumes = MediaType.APPLICATION_JSON_VALUE)
    public ChallengeResult submitChallenge(@RequestBody ChallengeSubmission challengeSubmission) {
        boolean result = false;
        String resultString;
        try {
            Challenge challenge = accessor.getEntity(challengeSubmission.challengeId)
                    .orElseThrow(() -> new Exception("Challenge " + challengeSubmission.challengeId + " not found"));
            ChallengeRunner runner = ChallengeRunnerFactory.getChallengeRunner(challenge.getLanguage());

            result = runner.challengeCodeValid(challenge, challengeSubmission.challengeCode);
            resultString = runner.lastOutput();
            if (result) AddNewCompletion(challenge, challengeSubmission.challengeCode);
        } catch (Exception e) {
            resultString = e.getMessage();
        });
        return new ChallengeResult(resultString, result);
    }

    private void AddNewCompletion(Challenge challenge, String code) {
        ChallengeCompletion completion = new ChallengeCompletion();
        completion.setChallengeID(challenge.getChallengeID());
        //TODO: Set user ID
        completion.setCode(code);
        completion.setCompletionTimeStamp(new Date());
        completionAccessor.saveEntity(completion);
    }

    @PostMapping(value = "/add")
    public boolean addChallenge(@RequestBody Challenge challenge) {
        challenge.setChallengeID(0);
        Challenge savedChallenge = SaveChallenge(challenge);
        return savedChallenge.getChallengeID() != 0;
    }

    // will also add if it doesn't exist
    @PostMapping(value = "/update")
    public boolean updateChallenge(@RequestBody Challenge challenge) {
        Challenge savedChallenge = SaveChallenge(challenge);
        return savedChallenge.getChallengeID() == challenge.getChallengeID();
    }

    private Challenge SaveChallenge(Challenge challenge) {
        return accessor.saveEntity(challenge);
    }

    @DeleteMapping(value = "/delete")
    public boolean updateChallenge(@RequestParam long challengeId) {
        return accessor.deleteEntity(challengeId);
    }//TODO: remove results for the challenge
}
