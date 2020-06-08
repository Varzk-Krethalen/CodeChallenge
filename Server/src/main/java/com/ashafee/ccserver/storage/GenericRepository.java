package com.ashafee.ccserver.storage;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.repository.NoRepositoryBean;

import javax.transaction.Transactional;
import java.util.List;
import java.util.Optional;

/*  This will be AUTO IMPLEMENTED by Spring into a Bean called genericRepository
    We then use this as a base for our individual repository types, so we can use
    the bean
        GenericRepository<MyEntity> dataRepository;
    instead of
        MyEntityRepository dataRepository;
    This reduces knowledge of implementation.
*/
@NoRepositoryBean @Transactional
public interface GenericRepository<T> extends JpaRepository<T, Long> {
    default boolean deleteEntity(long entityId) {
        deleteById(entityId);
        return !existsById(entityId);
    }
}
