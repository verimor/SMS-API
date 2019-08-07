package com.mycompany.mavenproject2;

import java.io.Serializable;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;

@Entity
public class Sonuc implements Serializable {
    @Id
    @GeneratedValue
    private int campaign_id;
    private String direction;
    private int campaign_custom_id;
    private int message_id;
    private int message_custom_id;
    private String dest;
    private int msg_size;
    private int international_multiplier;
    private int credits;
    private String status;
    private String gsm_error;
    private String sent_at;
    private String done_at;
    
    public int getCampaign_id() {
        return campaign_id;
    }

    public void setCampaign_id(int campaign_id) {
        this.campaign_id = campaign_id;
    }

    public String getDirection() {
        return direction;
    }

    public void setDirection(String direction) {
        this.direction = direction;
    }

    public int getCampaign_custom_id() {
        return campaign_custom_id;
    }

    public void setCampaign_custom_id(int campaign_custom_id) {
        this.campaign_custom_id = campaign_custom_id;
    }

    public int getMessage_id() {
        return message_id;
    }

    public void setMessage_id(int message_id) {
        this.message_id = message_id;
    }

    public int getMessage_custom_id() {
        return message_custom_id;
    }

    public void setMessage_custom_id(int message_custom_id) {
        this.message_custom_id = message_custom_id;
    }

    public String getDest() {
        return dest;
    }

    public void setDest(String dest) {
        this.dest = dest;
    }

    public int getMsgSize() {
        return msg_size;
    }

    public void setMsgSize(int msg_size) {
        this.msg_size = msg_size;
    }

    public int getInternational_multiplier() {
        return international_multiplier;
    }

    public void setInternational_multiplier(int international_multiplier) {
        this.international_multiplier = international_multiplier;
    }

    public int getCredits() {
        return credits;
    }

    public void setCredits(int credits) {
        this.credits = credits;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public String getGsm_error() {
        return gsm_error;
    }

    public void setGsm_error(String gsm_error) {
        this.gsm_error = gsm_error;
    }

    public String getSent_at() {
        return sent_at;
    }

    public void setSent_at(String sent_at) {
        this.sent_at = sent_at;
    }

    public String getDone_at() {
        return done_at;
    }

    public void setDone_at(String done_at) {
        this.done_at = done_at;
    }

    
    @Override
    public String toString() {
        return "Sonuc{" + "id=" + campaign_id + ", name=" + dest + ", address=" + gsm_error + '}';
    }

}