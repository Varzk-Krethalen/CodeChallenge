package com.ashafee.ccserver.storage;

import com.ashafee.ccserver.user.User;

import javax.transaction.Transactional;

@Transactional
public interface UserRepository extends GenericRepository<User> {
    User findByUsername(String username);
}