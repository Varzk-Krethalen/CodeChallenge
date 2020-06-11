package com.ashafee.ccserver.auth;

import com.ashafee.ccserver.storage.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.userdetails.User;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;

@Service
public class CustomUserDetailsService implements UserDetailsService {
    @Autowired
    UserRepository userAccessor;

    @Override
    public UserDetails loadUserByUsername(String username) {
        com.ashafee.ccserver.user.User user = userAccessor.findByUsername(username);
        if (user == null) {
            throw new UsernameNotFoundException(username);
        }
        return toUserDetails(user);
    }

    private UserDetails toUserDetails(com.ashafee.ccserver.user.User userObject) {
        return User.withUsername(userObject.getUsername())
                .password(userObject.getPassword())
                .roles(userObject.getUserType().toString()).build();
    }
}

