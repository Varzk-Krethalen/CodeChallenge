package com.ashafee.ccserver.handler;

import com.ashafee.ccserver.challenge.Challenge;
import com.ashafee.ccserver.ranking.Rank;
import com.ashafee.ccserver.ranking.Ranking;
import com.ashafee.ccserver.storage.ChallengeRepository;
import com.ashafee.ccserver.storage.CompletionRepository;
import com.ashafee.ccserver.storage.GenericRepository;
import com.ashafee.ccserver.storage.UserRepository;
import com.ashafee.ccserver.user.User;
import com.ashafee.ccserver.user.UserType;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.Sort;
import org.springframework.web.bind.annotation.*;

import javax.transaction.Transactional;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;
import java.util.Optional;
import java.util.function.Consumer;

@RestController @RequestMapping("/ranking") @Transactional
public class RankingHandler {
    @Autowired
    UserRepository userAccessor;
    @Autowired
    CompletionRepository completionAccessor;
    @Autowired
    ChallengeRepository challengeAccessor;

    @GetMapping("/allChallenges")
    public Ranking getRankingForAllChallenges() {
        List<Rank> ranks = new ArrayList<Rank>();
        for (User user : userAccessor.findAll()) {
            Rank rank = new Rank();
            rank.setChallengesCompleted(completionAccessor.countByUserID(user.getUserID()));
            if (rank.getChallengesCompleted() > 0)
            {
                rank.setObjectID(user.getUserID());
                rank.setObjectName(user.getUsername());
                ranks.add(rank);
            }
        }
        ranks.sort(Comparator.comparing(Rank::getChallengesCompleted));
        for (int i = 0; i < ranks.size(); i++) {
            ranks.get(i).setRank(i + 1);
        }
        return new Ranking("All challenges", ranks);
    }

    @GetMapping("/challenge")
    public Ranking getRankingByChallenge(@RequestParam long challengeId) {
        List<Rank> ranks = new ArrayList<Rank>();
        for (User user : userAccessor.findAll()) {
            Rank rank = new Rank();
            rank.setChallengesCompleted(completionAccessor.countByUserIDAndChallengeID(user.getUserID(), challengeId));
            if (rank.getChallengesCompleted() > 0)
            {
                rank.setObjectID(user.getUserID());
                rank.setObjectName(user.getUsername());
                ranks.add(rank);
            }
        }
        ranks.sort(Comparator.comparing(Rank::getChallengesCompleted));
        for (int i = 0; i < ranks.size(); i++) {
            ranks.get(i).setRank(i + 1);
        }
        return new Ranking("Challenge " + challengeId, ranks);
    }

    @GetMapping("/user")
    public Ranking getRankingByUser(@RequestParam long userId) {
        List<Rank> ranks = new ArrayList<Rank>();
        for (Challenge challenge : challengeAccessor.findAll()) {
            Rank rank = new Rank();
            rank.setChallengesCompleted(completionAccessor.countByUserIDAndChallengeID(userId, challenge.getChallengeID()));
            if (rank.getChallengesCompleted() > 0)
            {
                rank.setObjectID(challenge.getChallengeID());
                rank.setObjectName(challenge.getName());
                ranks.add(rank);
            }
        }
        ranks.sort(Comparator.comparing(Rank::getChallengesCompleted));
        for (int i = 0; i < ranks.size(); i++) {
            ranks.get(i).setRank(i + 1);
        }
        return new Ranking("User " + userId, ranks);
    }
}