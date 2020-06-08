package com.ashafee.ccserver.handler;

import com.ashafee.ccserver.ranking.Ranking;
import com.ashafee.ccserver.storage.CompletionRepository;
import com.ashafee.ccserver.storage.GenericRepository;
import com.ashafee.ccserver.storage.UserRepository;
import com.ashafee.ccserver.user.User;
import com.ashafee.ccserver.user.UserType;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import javax.transaction.Transactional;
import java.util.List;
import java.util.Optional;
import java.util.function.Consumer;

@RestController @RequestMapping("/ranking") @Transactional
public class RankingHandler {
    @Autowired
    UserRepository userAccessor;
    @Autowired
    CompletionRepository completionAccessor;


    @GetMapping("/all")
    public Ranking getRanking() {
        //userAccessor.
        return null;
    }

    @GetMapping("/challenge")
    public Ranking getRanking(@RequestParam long challengeId) {
        return null;
    }
}