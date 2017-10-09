#SMS API ile tek mesajın çok kişiye gönderimi örneği
#Aşağıdaki örnek kodu kendinize özelleştirerek kullanabilirsiniz.

import httplib
import json

def send_sms(header, message, phones):
  sms_msg = {
    'username' : '90850xxxxxxx', # https://oim.verimor.com.tr/sms_settings/edit adresinden öğrenebilirsiniz.
    'password' : 'sifreniz', # https://oim.verimor.com.tr/sms_settings/edit adresinden belirlemeniz gerekir.
    'source_addr' : header, # Gönderici başlığı, https://oim.verimor.com.tr/headers adresinde onaylanmış olmalı, değilse 400 hatası alırsınız.
    'messages' : [
      {'msg': message, 'dest': phones}
    ]
  }

  sms_msg = json.dumps(sms_msg)

  conn = httplib.HTTPConnection('sms.verimor.com.tr', 80)
  request_headers = {'Content-type': 'application/json'}
  conn.request("POST", "/v2/send.json", sms_msg, request_headers)
  response = conn.getresponse()
  print response.status, response.reason

  if response.status == 200:
    data = response.read()
    conn.close
    return data
  else:
    conn.close
    return false

source_addr = "BASLIGINIZ"
message = "deneme mesaj" # Bu metin UTF8 olmalı, değilse 400 hatası alırsınız. Veritabanından alınan string'ler, veritabanı bağlantısının encoding'iyle gelir, UTF8 değilse çevirmeniz gerekir.
dest = "905321112233,905321112234"

campaign_id = send_sms(source_addr, message, dest)
if campaign_id == false:
  print "Mesaj gönderilemedi."
else:
  print "Kampanya ID:", campaign_id
