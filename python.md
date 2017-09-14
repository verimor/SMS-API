**SMS API ile tek mesajın çok kişiye gönderimi örneği (python)**
----
Aşağıdaki örnek kodu kendinize özelleştirerek kullanabilirsiniz.
```python
import httplib
import json
 
def send_sms(header, message, phones):
  sms_msg = {
    'username' : '90850xxxxxxx',
    'password' : 'sifreniz',
    'source_addr' : header,
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
message = "deneme mesaj"
dest = "905321112233,905321112234"
 
campaign_id = send_sms(source_addr, message, dest)
if campaign_id == false:
  print "Mesaj gönderilemedi."
else:
  print "Kampanya ID:", campaign_id
```
