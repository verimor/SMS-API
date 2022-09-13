<%
'SMS API ile tek mesajın çok kişiye gönderimi örneği

'Aşağıdaki örnek kodu kendinize özelleştirerek kullanabilirsiniz.
' Sunucu IP adresinizi https://oim.verimor.com.tr/sms_settings/edit sayfasından girmiş olmanız gerekir.
' Girmezseniz 401 hatası alırsınız.
username = "xxxx" ' https://oim.verimor.com.tr/sms_settings/edit adresinden öğrenebilirsiniz.
password = "xxxx" ' https://oim.verimor.com.tr/sms_settings/edit adresinden belirlemeniz gerekir.
dest = "905321234567,905321234568"
source_addr = "BASLIGIM" ' Gönderici başlığı, https://oim.verimor.com.tr/headers adresinde onaylanmış olmalı, değilse 400 hatası alırsınız.
message = "Test Mesajıdır" ' Bu metin UTF8 olmalı, değilse 400 hatası alırsınız. Veritabanından alınan string'ler, veritabanı bağlantısının encoding'iyle gelir, UTF8 değilse çevirmeniz gerekir.

'json olusturuyoruz
jsonString = "{" &_
  """username"":""908505322000""," &_
  """password"":""aligel""," &_
  """source_addr"":"""&source_addr&"""," &_
  """messages"": [" &_
  " { " &_
  " ""msg"":"""&message&"""," &_
  " ""dest"":"""&dest&"""" &_
  " }" &_
  " ]" &_
  "}"
postUrl = "https://sms.verimor.com.tr/v2/send.json"
Set xml = Server.CreateObject("Microsoft.XMLHTTP")
xml.Open "POST", postUrl, false
xml.setRequestHeader "Content-Type", "application/json"
xml.SetRequestHeader "Codepage", "UTF-8"
xml.Send jsonString
response.Write jsonString

'gelen xml cevabi
response.Write xml.responseText
%>
