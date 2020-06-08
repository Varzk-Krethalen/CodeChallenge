package com.ashafee.ccserver.handler;

import com.ashafee.ccserver.storage.UserRepository;
import com.ashafee.ccserver.user.User;
import com.ashafee.ccserver.user.UserType;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.web.bind.annotation.*;

import javax.transaction.Transactional;
import java.util.List;
import java.util.Optional;
import java.util.function.Consumer;

@RestController @RequestMapping("/user") @Transactional
public class UserHandler {
    @Autowired
    UserRepository userAccessor;
    @Autowired
    public BCryptPasswordEncoder passwordEncoder;

    @GetMapping("/getAll")
    public List<User> getUsers() {
        return userAccessor.findAll();
    }

    @GetMapping("/getByName")
    public User getUserByName(@RequestParam String userName) {
        return userAccessor.findByUsername(userName);
    }

    @PostMapping(value = "/add")
    public boolean addUser(@RequestBody User user) {
        user.setUserID(0);
        user.setPassword(passwordEncoder.encode(user.getPassword()));
        User savedUser = userAccessor.save(user);
        return savedUser.getUserID() != 0;
    }

    @PostMapping(value = "/updateName")
    public boolean updateUserName(@RequestParam long userId, @RequestParam String userName) {
        return updateUser(userId, user -> user.setUsername(userName));
    }

    @PostMapping(value = "/updatePass")
    public boolean updateUserPass(@RequestParam long userId, @RequestParam String userPass) {
        return updateUser(userId, user -> user.setPassword(passwordEncoder.encode(userPass)));
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
    }
}