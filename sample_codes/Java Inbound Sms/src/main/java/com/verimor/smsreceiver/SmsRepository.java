/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.verimor.smsreceiver;

import javax.enterprise.context.ApplicationScoped;
import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import javax.transaction.Transactional;
import static javax.transaction.Transactional.TxType.REQUIRED;

/**
 *
 * @author Vasif Huseynov
 */
@ApplicationScoped
public class SmsRepository {
    @PersistenceContext(unitName = "InboundSms")
    private EntityManager em;

    @Transactional(REQUIRED)
    public void create(Sms sms) {
        em.persist(sms);
    }
}
