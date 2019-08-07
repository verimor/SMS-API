/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.verimor.smsreceiver;

import java.util.List;
import java.util.logging.Logger;
import javax.enterprise.context.ApplicationScoped;
import javax.inject.Inject;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.core.Response;

/**
 *
 * @author Vasif Huseynov
 */

@Path("/api/sms_receiver")
@ApplicationScoped

public class SmsReceiverController {
    private static final Logger LOG = Logger.getLogger(SmsReceiverController.class.getName());

    @Inject
    private SmsRepository smsRepository;

    @POST
    public Response createSms(List<Sms> smsler) {
        for (Sms sms: smsler){
            smsRepository.create(sms);
        }
        return Response.ok(smsler).build();
    }
}
