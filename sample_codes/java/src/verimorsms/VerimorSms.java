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
    private static SmsIstegi buildRequest()
    {
        SmsIstegi si = new SmsIstegi();
        si.username = "90850xxxxxxx"; // https://oim.verimor.com.tr/sms_settings/edit adresinden öğrenebilirsiniz.
        si.password = ""; // https://oim.verimor.com.tr/sms_settings/edit adresinden belirlemeniz gerekir.
        si.source_addr = "BAŞLIĞINIZ"; // Gönderici başlığı, https://oim.verimor.com.tr/headers adresinde onaylanmış olmalı, değilse 400 hatası alırsınız.
        si.messages = new Mesaj[]{ // Bu metin UTF8 olmalı, değilse 400 hatası alırsınız. Veritabanından alınan string'ler, veritabanı bağlantısının encoding'iyle gelir, UTF8 değilse çevirmeniz gerekir.
            new Mesaj("mesajınız","90535xxxxxxx,90532xxxxxxx")
        };

        return si;
    }

    private static void sendRequest(SmsIstegi smsIstegi)
    {
        Gson gson = new Gson();
        String payload = gson.toJson(smsIstegi);

        try {
            URL url = new URL("https://sms.verimor.com.tr/v2/send.json");
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

            if (connection.getResponseCode() < HttpURLConnection.HTTP_BAD_REQUEST){
                InputStream istr = connection.getInputStream();
                String campaign_id = readStream(istr);

                System.out.println("Kampanya id:");
                System.out.println(campaign_id);
                istr.close();
            } else {
                InputStream istr = connection.getErrorStream();
                String responseBody = readStream(istr);

                System.out.println("Mesaj gönderilemedi, dönen hata: ");
                System.out.println(responseBody);
                istr.close();
            }

            connection.disconnect();
        } catch (IOException ex) {
            System.err.println(ex);
            ex.printStackTrace();
        }
    }

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        // Sunucu IP adresinizi https://oim.verimor.com.tr/sms_settings/edit sayfasından girmiş olmanız gerekir.
        // Girmezseniz 401 hatası alırsınız.

        SmsIstegi smsIstegi = buildRequest();
        sendRequest(smsIstegi);
    }

    private static String readStream(InputStream istr) throws IOException {
        String result = "";
        int c;
        while ((c = istr.read()) != -1){
            result = result.concat(String.valueOf((char)c));
        }

        return result;
    }
}
