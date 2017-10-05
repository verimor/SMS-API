'SMS API ile tek mesajın çok kişiye gönderimi örneği
'Aşağıdaki örnek kodu kendinize özelleştirerek kullanabilirsiniz.

<% username = "xxxx" password = "xxxx" dest = "905321234567,905321234568" source_addr = "BASLIGIM" message = "Test Mesajıdır"
'json olusturuyoruz 
jsonString = "{"&_ """username"":""908505322000""," &_ """password"":""aligel""," &_ """source_addr"":"""&source_addr&"""," &_ """messages"": [" &_ " { " &_ " ""msg"":"""&message&"""," &_ " ""dest"":"""&dest&"""" &_ " }" &_ " ]" &_ "}" 
postUrl = "http://sms.verimor.com.tr/v2/send.json" 
Set xml = Server.CreateObject("Microsoft.XMLHTTP") xml.Open "POST",
postUrl , false xml.setRequestHeader "Content-Type", "application/json" xml.SetRequestHeader "Codepage", "UTF-8" xml.Send 
jsonString response.Write jsonString
'gelen xml cevabi 
response.Write xml.responseText %>
