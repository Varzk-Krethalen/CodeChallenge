package com.ashafee.ccserver.storage;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import javax.transaction.Transactional;
import java.util.List;
import java.util.Optional;

@Service @Transactional
public class JPADataAccessor<T> {
    @Autowired
    private GenericRepository<T> dataRepository;

    public List<T> getAllEntities() {
        return dataRepository.findAll();
    }

    public Optional<T> getEntity(Number entityID) {
        return dataRepository.findById(entityID.longValue());
    }

    public T saveEntity(T entity) {
        return dataRepository.save(entity);
    }
}
