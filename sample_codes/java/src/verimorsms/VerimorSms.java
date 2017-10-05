/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package verimorsms;

import com.google.gson.Gson;
import java.io.*;
import java.net.*;

class Mesaj
{
    String msg;
    String dest;

    public Mesaj() { }

    public Mesaj(String msg, String dest)
    {
        this.msg = msg;
        this.dest = dest;
    }
}

class SmsIstegi {
    public String username;
    public String password;
    public String source_addr;
    public Mesaj[] messages;
}

/**
 *
 * @author fatih
 */
public class VerimorSms {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        try{
            SmsIstegi si = new SmsIstegi();
            si.username = "90850xxxxxxx";
            si.password = "";
            si.source_addr = "BAŞLIĞINIZ";
            si.messages = new Mesaj[]{
                new Mesaj("mesajınız","90535xxxxxxx,90532xxxxxxx")
            };
            
            Gson gson = new Gson();
            String payload = gson.toJson(si);
            
            URL url = new URL("http://sms.verimor.com.tr/v2/send.json");
            HttpURLConnection connection = (HttpURLConnection) url.openConnection();
            connection.setDoOutput(true);
            connection.setDoInput(true);
            connection.setRequestMethod("POST");
            connection.setRequestProperty("Content-Type", "application/json");
            OutputStream ostr = connection.getOutputStream();
            OutputStreamWriter ostrw = new OutputStreamWriter(ostr, "UTF-8");
            ostrw.write(payload);
            ostrw.flush();
            ostr.close();
            
            InputStream istr = connection.getInputStream();
            String campaign_id = "";
            int c;
            while ((c = istr.read()) != -1){
                campaign_id = campaign_id.concat(String.valueOf((char)c));
            }
            System.out.println("Kampanya id:");
            System.out.println(campaign_id);
            istr.close();
            ostr.close();
            connection.disconnect();
        }
        catch (IOException ex)
        {
            System.err.println(ex);
            ex.printStackTrace();
        }
    }
}