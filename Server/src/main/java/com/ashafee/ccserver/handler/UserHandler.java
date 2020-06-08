package com.ashafee.ccserver.handler;

import com.ashafee.ccserver.storage.GenericRepository;
import com.ashafee.ccserver.user.User;
import com.ashafee.ccserver.user.UserType;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.web.bind.annotation.*;

import javax.transaction.Transactional;
import java.util.List;
import java.util.Optional;
import java.util.function.Consumer;

@RestController @RequestMapping("/user") @Transactional
public class UserHandler {
    @Autowired
    GenericRepository<User> userAccessor;

    @GetMapping("/getAll")
    public List<User> getUsers() {
        return userAccessor.findAll();
    }

    @PostMapping(value = "/add")
    public boolean addUser(@RequestBody User user) {
        user.setUserID(0);
        User savedUser = userAccessor.save(user);
        return savedUser.getUserID() != 0;
    }

    @PostMapping(value = "/updateName")
    public boolean updateUserName(@RequestParam long userId, @RequestParam String userName) {
        return updateUser(userId, user -> user.setUsername(userName));
    }

    @PostMapping(value = "/updatePass")
    public boolean updateUserPass(@RequestParam long userId, @RequestParam String userPass) {
        return updateUser(userId, user -> user.setPassword(userPass));
    }

    @PostMapping(value = "/updateType")
    public boolean updateUserType(@RequestParam long userId, @RequestParam UserType userType) {
        return updateUser(userId, user -> user.setUserType(userType));
    }

    private boolean updateUser(long userId, Consumer<User> userAction) {
        Optional<User> userOptional = userAccessor.findById(userId);
        if (userOptional.isPresent())
        {
            User savedUser = userOptional.get();
            userAction.accept(savedUser);
            return true;
        }
        return false;
    }

    @DeleteMapping(value = "/delete")
    public boolean deleteUser(@RequestParam long userId) {
        return userAccessor.deleteEntity(userId);
    }//TODO: remove results for the user
}