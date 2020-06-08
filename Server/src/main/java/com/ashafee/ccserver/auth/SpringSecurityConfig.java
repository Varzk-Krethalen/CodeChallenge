package com.ashafee.ccserver.auth;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Configuration;
import org.springframework.http.HttpMethod;
import org.springframework.security.config.annotation.authentication.builders.AuthenticationManagerBuilder;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.WebSecurityConfigurerAdapter;

//adapted from https://mkyong.com/spring-boot/spring-rest-spring-security-example/
@Configuration
public class SpringSecurityConfig extends WebSecurityConfigurerAdapter {
    @Autowired
    private CustomUserDetailsService userDetailsService;

    // Create 2 users for demo
    @Override
    protected void configure(AuthenticationManagerBuilder auth) throws Exception {
        auth.userDetailsService(userDetailsService);
    }

    // Secure the endpoints with HTTP Basic authentication
    @Override
    protected void configure(HttpSecurity http) throws Exception {
        http
            //HTTP Basic authentication
            .httpBasic()
            .and()
            .authorizeRequests()
            .antMatchers(HttpMethod.GET, "/**/get*").hasRole("USER")
            .antMatchers(HttpMethod.POST, "/**/submit").hasRole("USER")
            .antMatchers(HttpMethod.POST, "/**/add*").hasRole("ADMIN")
            .antMatchers(HttpMethod.POST, "/**/update*").hasRole("ADMIN")
            .antMatchers(HttpMethod.DELETE, "/**/delete*").hasRole("ADMIN")
            .antMatchers(HttpMethod.GET, "/**/**").hasRole("ADMIN")
            .antMatchers(HttpMethod.POST, "/**/**").hasRole("ADMIN")
            .antMatchers(HttpMethod.PUT, "/**/**").hasRole("ADMIN")
            .antMatchers(HttpMethod.PATCH, "/**/**").hasRole("ADMIN")
            .antMatchers(HttpMethod.DELETE, "/**/**").hasRole("ADMIN")
            .and()
            .csrf().disable()
            .formLogin().disable();
    }
}