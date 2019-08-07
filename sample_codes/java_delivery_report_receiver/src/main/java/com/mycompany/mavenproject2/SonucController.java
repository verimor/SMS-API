package com.mycompany.mavenproject2;

import java.util.List;
import java.util.logging.Logger;
import javax.enterprise.context.ApplicationScoped;
import javax.inject.Inject;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.core.Response;

@Path("/api/sonuc")
@ApplicationScoped

public class SonucController {

    private static final Logger LOG = Logger.getLogger(SonucController.class.getName());

    @Inject
    private SonucRepository sonucRepository;

    @POST
    public Response createSonuc(List<Sonuc> sonuclar) {
        for (Sonuc sonuc: sonuclar){
            sonucRepository.create(sonuc);
        }
        return Response.ok(sonuclar).build();
    }

}