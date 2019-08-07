    
package com.mycompany.mavenproject2;

import javax.enterprise.context.ApplicationScoped;
import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import javax.transaction.Transactional;
import static javax.transaction.Transactional.TxType.REQUIRED;

@ApplicationScoped
public class SonucRepository {

    @PersistenceContext(unitName = "SAMPLE_PU")
    private EntityManager em;

    @Transactional(REQUIRED)
    public void create(Sonuc sonuc) {
        em.persist(sonuc);
    }
}