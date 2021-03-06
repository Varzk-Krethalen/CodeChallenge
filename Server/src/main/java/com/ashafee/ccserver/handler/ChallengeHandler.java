package com.ashafee.ccserver.handler;

import com.ashafee.ccserver.challenge.*;
import com.ashafee.ccserver.handler.commsobjects.ChallengeResult;
import com.ashafee.ccserver.handler.commsobjects.ChallengeSubmission;
import com.ashafee.ccserver.storage.ChallengeRepository;
import com.ashafee.ccserver.storage.CompletionRepository;
import com.ashafee.ccserver.storage.UserRepository;
import com.ashafee.ccserver.user.User;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.web.bind.annotation.*;

import javax.transaction.Transactional;
import java.util.Date;
import java.util.List;

@RestController @RequestMapping("/challenge") @Transactional
public class ChallengeHandler {
    @Autowired
    ChallengeRepository challengeAccessor;
    @Autowired
    CompletionRepository completionAccessor;
    @Autowired
    UserRepository userAccessor;

    @GetMapping("/getAll")
    public List<Challenge> getChallenges() {
        return challengeAccessor.findAll();
    }

    @PostMapping(value = "/submit", consumes = MediaType.APPLICATION_JSON_VALUE)
    public ChallengeResult submitChallenge(@RequestBody ChallengeSubmission challengeSubmission) {
        boolean result = false;
        String resultString;
        try {
            Challenge challenge = challengeAccessor.findById(challengeSubmission.challengeId)
                    .orElseThrow(() -> new Exception("Challenge " + challengeSubmission.challengeId + " not found"));
            ChallengeRunner runner = ChallengeRunnerFactory.getChallengeRunner(challenge.getLanguage());

            result = runner.challengeCodeValid(challenge, challengeSubmission.challengeCode);
            resultString = runner.lastOutput();
            if (result) AddNewCompletion(challenge, challengeSubmission.challengeCode);
        } catch (Exception e) {
            resultString = e.getMessage();
        }
        return new ChallengeResult(resultString, result);
    }

    private void AddNewCompletion(Challenge challenge, String code) {
        ChallengeCompletion completion = new ChallengeCompletion();
        completion.setChallengeID(challenge.getChallengeID());
        completion.setUserID(getCurrentUser().getUserID());
        completion.setCode(code);
        completion.setCompletionTimeStamp(new Date());
        completionAccessor.save(completion);
    }

    private User getCurrentUser() {
        String username;
        Object principal = SecurityContextHolder.getContext().getAuthentication().getPrincipal();
        if (principal instanceof UserDetails) {
            username = ((UserDetails)principal).getUsername();
        } else {
            username = principal.toString();
        }
        return userAccessor.findByUsername(username);
    }

    @PostMapping(value = "/add")
    public boolean addChallenge(@RequestBody Challenge challenge) {
        challenge.setChallengeID(0);
        Challenge savedChallenge = challengeAccessor.save(challenge);
        return savedChallenge.getChallengeID() != 0;
    }

    // will also add if it doesn't exist
    @PostMapping(value = "/update")
    public boolean updateChallenge(@RequestBody Challenge challenge) {
        Challenge savedChallenge = challengeAccessor.save(challenge);
        return savedChallenge.getChallengeID() == challenge.getChallengeID();
    }

    @DeleteMapping(value = "/delete")
    public boolean deleteChallenge(@RequestParam long challengeId) {
        return challengeAccessor.deleteEntity(challengeId);
    }
}
