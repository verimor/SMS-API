<%
' Sunucu IP adresinizi https://oim.verimor.com.tr/sms_settings/edit sayfasından girmiş olmanız gerekir.
' Girmezseniz 401 hatası alırsınız.
username ="kullanıcı_adınız"
password =  Server.URLEncode("sifreniz")
url = "https://sms.verimor.com.tr/v2/balance?username=" & username &"&"&"password="&password

Set HttpReq = Server.CreateObject("MSXML2.ServerXMLHTTP")
HttpReq.open "GET", url, false
HttpReq.Send()
sonuc = HttpReq.responseText

If  IsNumeric(sonuc) and sonuc <>"" Then
	response.write("______ Kalan Sms Kredi Miktarı :<b> <font color='red'> "  & sonuc & "<b></font><br>")
	response.write("______ <b>Not: 100.000 'den az kaldığında lütfen birim amirinize bilgi veriniz.<b>")
Else
	response.write("Kredi sorgulaması yapılamadı.Kullanıcı adınızı ve şifrenizi kontrol ediniz.")
End If

%>
