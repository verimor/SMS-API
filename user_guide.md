
# **VERİMOR SMS API DÖKÜMANI**

Bu doküman, Verimor SMS API (kısaca Smsapi) ile mesaj gönderiminin nasıl yapılacağını, ileri tarihli gönderimin nasıl iptal edileceğini, gönderim raporunun ve gelen smslerinizin nasıl alınacağını anlatır.

Smsapi ile sms göndermek için iki bilgiye ihtiyaç vardır: <br/>
1- Verimor hesabınızın kullanıcı adı (12 haneli telefon numaranız, 908501234567 gibi) <br/>
2- API şifreniz ([OİM üzerinden](https://oim.verimor.com.tr/sms_settings/edit) tanımlayabilirsiniz).<br/>
3- Gönderim yapacağınız sunucunun IP adresi. BTK yönergeleri kapsamında alınan güvenlik önlemleri sebebiyle SMS göndereceğiniz sunucunuzun IP adresini [SMS Ayarlarım](https://oim.verimor.com.tr/sms_settings/edit) sayfasına girmeniz zorunludur. IP adresinizi girmezseniz `401 Hesabınızda izinli IP ayarları yapılmamış` hatası alırsınız.

----
**İÇİNDEKİLER** 
----
* [SMS GÖNDERİMİ](#sms-g%C3%B6nderi%CC%87mi%CC%87)
* [İLERİ TARİHLİ MESAJ GÖNDERİMİ İPTALİ](#i%CC%87leri%CC%87-tari%CC%87hli%CC%87-mesaj-g%C3%B6nderi%CC%87mi%CC%87-i%CC%87ptali%CC%87)
* [GÖNDERİM RAPORU ALIMI](#g%C3%B6nderi%CC%87m-raporu-alimi)
* [GELEN SMS ALIMI](#gelen-sms-alimi)
* [HTTP GET İLE SMS BAŞLIK LİSTESİ ALIMI](#http-get-i%CC%87le-sms-ba%C5%9Flik-li%CC%87stesi%CC%87-alimi)
* [HTTP GET İLE KARALİSTENİZDEKİ NUMARALARIN ALIMI](#http-get-i%CC%87le-karali%CC%87steni%CC%87zdeki%CC%87-numaralarin-alimi)
* [HTTP POST İLE KARALİSTENİZE NUMARA EKLENMESİ](#http-post-i%CC%87le-karali%CC%87steni%CC%87ze-numara-eklenmesi%CC%87)
* [HTTP DELETE İLE KARALİSTENİZDEKİ NUMARANIN SİLİNMESİ](#http-delete-i%CC%87le-karali%CC%87steni%CC%87zdeki%CC%87-numaranin-si%CC%87li%CC%87nmesi%CC%87)
* [İYS İZNİ GÖNDERİMİ](#i%CC%87ys-i%CC%87zni%CC%87-g%C3%B6nderi%CC%87mi%CC%87)
* [İYS GÜNLÜK RAPOR ALIMI](#i%CC%87ys-g%C3%BCnl%C3%BCk-rapor-alimi)
* [HATA KODLARI](#hata-kodlari)
* [SMS BOY KARAKTER LİMİTLERİ](#sms-boy-karakter-li%CC%87mi%CC%87tleri%CC%87)

----
**SMS GÖNDERİMİ**
----
Smsapi gönderim için iki yöntemi destekler. Bunlar **HTTP(S) GET** (Plain de denir) ve **HTTP(S) POST JSON**’dır. İkisi de cevabını düz metin olarak döndürür.

**HTTP GET ile SMS Gönderimi**

Aşağıdaki örnekte olduğu gibi bir URL çağırılır.

**Örnek:**
> http://sms.verimor.com.tr/v2/send?username=908501234567&password=xxxxxxx&source_addr=BASLIGIM&msg=deneme12&dest=905311234567,905319876543&datacoding=0&valid_for=2:00

* username: Verimor hesabınızın kullanıcı adı. (zorunlu)
* password: API şifreniz. (zorunlu)
* source_addr: Gönderici kimliği (Başlık). Source_addr boş ise sistemde kayıtlı ilk başlığınız kullanılır.
* msg: Gönderilecek mesaj. Türkçe harf içerebilir. Maksimum uzunluğu Türkçe harf içeriyorsa 1043, içermiyorsa 1071 karakter’dir. (zorunlu). Encoding her zaman UTF8 beklenir.
* dest: Mesajın gönderileceği telefon numaraları. Birden fazla numara varsa virgül ile ayrılmalıdır. Yurt dışı numaralarının başına 00 veya + eklenmelidir. Örnek: 0049xxxxxxxx veya +49xxxxxxxx (+ işareti URL'lerde boşluk olarak yorumlanacağı için %2B olarak encode etmelisiniz, örn: %2B49xxxxxxxx). (zorunlu)
* valid_for: Mesajın geçerlilik süresi. SS:DD (veya S:DD) formatında olmalı. (Varsayılan değer 24:00, Minumum değer 00:01, Maksimum değer 48:00)
* send_at: Mesajın gönderilmesini istediğiniz tarih saat. ‘2015-02-20 16:06:00’ şeklinde veya ISO 8601 standardındaki formatlar kabul edilir (http://en.wikipedia.org/wiki/ISO_8601). Boş ise mesaj hemen gönderilir.
* datacoding: Mesaj metni için kullanılacak karakter kodlaması. 0, 1 ve 2 değerlerini alabilir. Mesajda kullanılabilecek harfleri ve mesajın boy limitlerini belirler. Boş ise mesaj metnine bakılır, türkçe harf varsa 1, yoksa 0 kaydedilir. Mesaj boyları tablosu için dokümanın sonuna bakınız. Yurt dışına sms gönderiminde değeri 1 olarak gönderilmemelidir.
* is_commercial: Opsiyonel. true | false değeri alır. Ticari gönderimlerde true olarak belirlemelisiniz.

**Cevap (Başarılı):**
```json
HTTP/1.1 200 OK
20210
```

**Cevap (Başarısız):**
```json
HTTP/1.1 400 Bad Request
INSUFFICIENT_CREDITS
```

Gönderim başarısızsa; cevap olarak “HTTP/1.1 400 Bad Request” mesajı ve kampanya ID’si yerine hata mesajı döner. “HTTP/1.1 400 Bad Request” mesajı HTTP response header kısmında olup cevap metninde (response body) geçmez.

**HTTP POST JSON ile SMS Gönderimi** 

Aşağıdaki örnekte olduğu gibi bir JSON string POST edilir.

```json
POST http://sms.verimor.com.tr/v2/send.json
Host: sms.verimor.com.tr
Content-Type: application/json
Accept: */*
 
{
  "username"    : "908501234567",
  "password"    : "xxxxxxx",
  "source_addr" : "BASLIGIM",
  "valid_for"   : "48:00",
  "send_at"     : "2015-02-20 16:06:00",
  "custom_id"   : "123456789",
  "datacoding"  : "0",
  "messages": [
               { 
                "msg" : "deneme123",
                "dest": "905311234567,905319876543",
                "id"  : "1234,1235"
               }
              ]
}
```
* username: Verimor hesabınızın kullanıcı adı. (zorunlu)
* password: API şifreniz. (zorunlu)
* source_addr: Gönderici kimliği (Başlık). Source_addr boş ise sistemde kayıtlı ilk başlığınız kullanılır.
* valid_for: Mesajın geçerlilik süresi. SS:DD (veya S:DD) formatında olmalı. (Varsayılan değer 24:00, Minumum değer 00:01, Maksimum değer 48:00)
* send_at: Mesajın gönderilmesini istediğiniz tarih saat. ‘2015-02-20 16:06:00’ şeklinde veya ISO 8601 standardındaki formatlar kabul edilir (http://en.wikipedia.org/wiki/ISO_8601). Boş ise mesaj hemen gönderilir.
* custom_id: Bu kampanyaya verebileceğiniz özel ID’dir. API’nin kampanyaya döndüreceği ID’yi kullanmayıp kendi vereceğiniz ID ile mesajların sonucu takip etmek isterseniz kullanılır. (Push veya GET ile Gönderim Raporu alırken bu ID’yi kullanabilirsiniz.)(zorunlu değil)
* msg: Gönderilecek mesaj. Türkçe harf içerebilir. Maksimum uzunluğu Türkçe harf içeriyorsa 1043, içermiyorsa 1071 karakter’dir. (zorunlu). Encoding her zaman UTF8 beklenir.
* dest: Mesajın gönderileceği telefon numaraları. Birden fazla numara varsa virgül ile ayrılmalıdır. (zorunlu)
* id: Kampanyanın içindeki mesajlara verebileceğiniz özel ID'lerdir. Push ile gönderim raporu alırken bu ID'yi kullanabilirsiniz. "dest" parametresinde birden fazla numara varsa, o kadar id verilmelidir, yoksa bu parametre dikkate alınmaz.
* datacoding: Mesaj metni için kullanılacak karakter kodlaması. 0, 1 ve 2 değerlerini alabilir. Mesajda kullanılabilecek harfleri ve mesajın boy limitlerini belirler. Boş ise mesaj metnine bakılır, türkçe harf varsa 1, yoksa 0 kaydedilir. Mesaj boyları tablosu için dokümanın sonuna bakınız. Yurt dışına sms gönderiminde bu parametrenin kullanılmaması gerekmektedir.
* is_commercial: Opsiyonel. true | false değeri alır. Ticari gönderimlerde true olarak belirlemelisiniz.


**Cevap:**
```json
HTTP/1.1 200 OK
20212
```
----
**İLERİ TARİHLİ MESAJ GÖNDERİMİ İPTALİ**
----
İleri tarihli mesaj gönderimini iptal etmek için aşağıdaki örnekte olduğu gibi bir JSON string POST edilir.

```json
POST http://sms.verimor.com.tr/v2/cancel/20121
Host: sms.verimor.com.tr
Content-Type: application/json
Accept: */*
 
{
  "username"    : "908501234567",
  "password"    : "xxxxxxx"
}
```
**Cevap (Başarılı):**
```json
HTTP/1.1 200 OK
Kampanya silindi: 20121
```

**Cevap (Başarısız):**
```json
HTTP/1.1 400 Bad Request
Kampanya bulunamadı: 20121
```
----
**GÖNDERİM RAPORU ALIMI**
----
Smsapi gönderim raporlarını iki şekilde teslim eder. Bunlar PUSH ve GET yöntemleridir.

**PUSH ile Gönderim Raporu Alımı**

Push yönteminde mesajın durumu ile ilgili bilgi (teslim edildi, zaman aşımı, numara hatalı vb.) alınır alınmaz [OİM üzerinden](https://oim.verimor.com.tr/sms_settings/edit) daha önce belirlediğiniz bir URL tetiklenir. Aşağıdaki gibi bir JSON POST edilir:
```json
POST http://sizin.adresiniz.com.tr/sms_push
Host: sizin.adresiniz.com.tr
Content-Type: application/json
Accept: */*
 
[
 {
  "type"                     : "outbound",
  "campaign_id"              : 20121,
  "campaign_custom_id"       : "123456789",
  "message_id"               : "13582302",
  "message_custom_id"        : "1234",
  "dest"                     : "905319876543",
  "size"                     : 1,
  "international_multiplier" : 1,
  "credits"                  : 1,
  "status"                   : "DELIVERED",
  "gsm_error"                : "0",
  "sent_at"                  : "2015-02-20 16:06:00",
  "done_at"                  : "2015-02-20 16:06:00"
 }
 ,
 {
  "type"                     : "outbound",
  "campaign_id"              : 20121,
  "campaign_custom_id"       : "123456789",
  "message_id"               : "13582303",
  "message_custom_id"        : "1235",
  "dest"                     : "905319876544",
  "size"                     : 1,
  "international_multiplier" : 1,
  "credits"                  : 1,
  "status"                   : "DELIVERED",
  "gsm_error"                : "0",
  "sent_at"                  : "2015-02-20 16:06:00",
  "done_at"                  : "2015-02-20 16:06:00"
 }
]
```
* campaign_id: Mesajın kampanya ID’si.
* type: Mesajın yönüdür. Gönderilen sms olduğu için outbound
* campaign_custom_id: Mesajın kampanyasına sizin tarafınızdan verilmiş özel ID.
* message_id: Mesaja API tarafından verilmiş ID.
* message_custom_id: Mesaja sizin tarafınızdan verilmiş özel ID.
* dest: Mesajın gönderildiği telefon numarası. Yurt dışı numaralarının başına 00 eklenmelidir. Örnek: 0049xxxxxxxx.
* size: Mesajın boyu.
* international_multiplier: Mesajın kredi çarpanı (bir boyunun kaç krediye denk geldiği). Uluslararası mesajlarda 1’den büyük olur. Ulusal mesajlarda daima 1 olur.
* credits: Bu mesaj için hesabınızdan kaç kredi düşüldüğü.
* status: Mesajın durumu (olabilecek durumlar ve anlamları için dokümanın sonundaki durum listesine bakınız).
* gsm_error: Mesaj iletilemediyse operatörden dönen hata kodu.
* sent_at: Mesajın iletildiği tarih (mesaj iletilemediyse null olur)
* done_at: Mesajın son durumuna ulaştığı tarih (mesaj iletilemediyse de dolu olur)

Not: API, sisteminize PUSH bildirimi yaptığında “HTTP/1.1 200 OK” cevabı bekler. Bu cevabı alamadığı zaman 5’er dakika bekleyerek 3 kere daha dener. Hala cevap alamazsa bu mesajı tekrar bildirmez.

**HTTP GET ile Gönderim Raporu Alımı**<br/>
Kampanyaların durumunu GET ile sorarak da alabilirsiniz. Örnekler;

**Örnek (API’nin ürettiği ID ile sorma):**
>http://sms.verimor.com.tr/v2/status?id=20121&username=908501234567&password=xxxx

**Örnek (Custom ID ile sorma):**
>http://sms.verimor.com.tr/v2/status?custom_id=123456789&username=908501234567&password=xxxx

**Örnek (Cep telefonuna göre sorma):**
>http://sms.verimor.com.tr/v2/status?id=20121&dest=905319876543,905319876544&username=908501234567&password=xxxx

**Örnek (message_id belirli değerden büyük olanları sorgulama):**
>http://sms.verimor.com.tr/v2/status?id=20121&greater_than=13582302&username=908501234567&password=xxxx

* id: Kampanya’ya API tarafından verilen ID’dir. id veya custom_id zorunludur.
* custom_id: Kampanya’ya sizin tarafınızdan verilen ID’dir. id veya custom_id zorunludur.
* dest: Zorunlu değil. Kampanya’da belirli telefon numaralarına gönderilmiş mesajları sorgular.
* greater_than: Verilen message_id’den büyük mesajları sorgular. Bu parametre, içinde çok mesaj olan kampanyaların sorgulanması için zorunludur. Bu sorgu 100 mesaj döndürür, mesajların devamını almak için sonuçtaki son mesaj id’sini vererek ikinci bir sorgu yapmalısınız.

**Cevap (Başarılı):**
```json
HTTP/1.1 200 OK
 
[
 {
  "campaign_id"              : 20121,
  "campaign_custom_id"       : "123456789",
  "message_id"               : "13582302",
  "message_custom_id"        : "1234",
  "dest"                     : "905319876543",
  "size"                     : 1,
  "international_multiplier" : 1,
  "credits"                  : 1,
  "status"                   : "DELIVERED",
  "gsm_error"                : "0",
  "sent_at"                  : "2015-02-20 16:06:00",
  "done_at"                  : "2015-02-20 16:06:00"
 }
 ,
 {
  "campaign_id"              : 20121,
  "campaign_custom_id"       : "123456789",
  "message_id"               : "13582303",
  "message_custom_id"        : "1235",
  "dest"                     : "905319876544",
  "size"                     : 1,
  "international_multiplier" : 1,
  "credits"                  : 1,
  "status"                   : "DELIVERED",
  "gsm_error"                : "0",
  "sent_at"                  : "2015-02-20 16:06:00",
  "done_at"                  : "2015-02-20 16:06:00"
 }
]
```
**Cevap (Başarısız. Verdiğiniz ID’ye ait kampanya size ait değilse):**
```json
HTTP/1.1 401 Forbidden
Bu kampanya size ait değil
```
**Cevap (Başarısız. Verdiğiniz ID’ye ait kampanya bulunamadıysa):**
```json
HTTP/1.1 404 Not Found
Bu idye sahip kampanya bulunamadı
```
----
**GELEN SMS ALIMI**
----
Smsapi hesabınıza gelen sms’leri iki farklı yöntemle teslim edebilir. Bunlar PUSH ve GET yöntemleridir.

**PUSH ile Gelen SMS Alımı** <br/>
Push yönteminde hesabınıza bir mesaj gelir gelmez [OİM üzerinden](https://oim.verimor.com.tr/sms_settings/edit) daha önce belirlediğiniz bir URL tetiklenir. Aşağıdaki gibi bir JSON POST edilir:
```json
POST http://sizin.adresiniz.com.tr/sms_push
Host: sizin.adresiniz.com.tr
Content-Type: application/json
Accept: */*
 
[
 {
  "message_id"       : 1234,
  "type"        : "inbound",
  "received_at"      : "2017-01-01 09:00:00",
  "network"          : "TURKCELL",
  "source_addr"      : "905319876543",
  "destination_addr" : "908501234567",
  "keyword"          : "",
  "content"          : "verimor deneme"
 }
]
```
* type: Mesajın yönüdür. Gelen sms olduğu için inbound
* received_at: Mesajın alındığı tarih saat
* network: Mesajı gönderen operatör. TURKCELL, TTMOBIL, VODAFONE değerleri olabilir.
* source_addr: Mesajı gönderen numara
* destination_addr: Mesajın gönderildiği numara (Verimor abone numarası veya 4 haneli Verimor ücretsiz kısa numarası)
* keyword: Ortak kullanımlı kısa numaralardaki ayırt edici anahtar kelime. Kısa numaraya değil doğrudan sizin numaranıza gelen sms'lerde boş olur.
* content: Gelen mesajın tam içeriği

**Not:** API, sisteminize PUSH bildirimi yaptığında “HTTP/1.1 200 OK” cevabı bekler. Bu cevabı alamadığı zaman 5’er dakika bekleyerek 3 kere daha dener. Hala cevap alamazsa bu mesajı tekrar bildirmez.

**HTTP GET ile Gelen SMS Alımı**<br/>
Gelen SMS’lerinizi GET ile sorarak da alabilirsiniz. Örnekler;

**Örnek (Tarih aralığına göre sorma):**
>http://sms.verimor.com.tr/v2/inbound_messages?from_time=2017-01-01 09:00:00&to_time=2017-01-01 12:00:00&username=908501234567&password=xxxx

**Örnek (message_id belirli değerden büyük olanları sorgulama):**
>http://sms.verimor.com.tr/v2/inbound_messages?greater_than=13582302&username=908501234567&password=xxxx

* from_time: Sorgulanacak zaman aralığının başlangıcı
* to_time: Sorgulanacak zaman aralığının bitişi
* greater_than: Verilen message_id’den büyük mesajları sorgular. Bu sorgu 100 mesaj döndürür, mesajların devamını almak için sonuçtaki son mesaj id’sini vererek ikinci bir sorgu yapmalısınız.

**Cevap:**
```json
HTTP/1.1 200 OK
 
[
 {
  "message_id"       : 1234,
  "received_at"      : "2017-01-01T09:00:00.000+03:00",
  "network"          : "TURKCELL",
  "source_addr"      : "905335876543",
  "destination_addr" : "4609",
  "keyword"          : "verimor",
  "content"          : "verimor deneme"
 }
,
 {
  "message_id"       : 1235,
  "received_at"      : "2017-01-01T10:00:00.000+03:00",
  "network"          : "VODAFONE",
  "source_addr"      : "905444876543",
  "destination_addr" : "908501234567",
  "keyword"          : "",
  "content"          : "1234 numaralı siparişim kargoya verildi mi?"
 }
]
```
**Cevap (Hiç mesajı olmadığı durumda):**
```json
HTTP/1.1 200 OK
[]
```

---
**HTTP GET İLE SMS BAŞLIK LİSTESİ ALIMI**
---
Aşağıdaki örnekte olduğu gibi bir URL çağırılır.

**Örnek:**
>http://sms.verimor.com.tr/v2/headers?username=908501234567&password=xxxx

**Cevap (Başarılı):**
```json
HTTP/1.1 200 OK
["Verimor TLK", "Bulutsantralim"]
```

**Cevap (Başarısız):**
```json
HTTP/1.1 401 Unauthorized
Geçersiz kullanıcı adı/şifre
```


---
**HTTP GET İLE KARALİSTENİZDEKİ NUMARALARIN ALIMI**
---
Aşağıdaki örnekte olduğu gibi bir URL çağırılır.

**Örnek:**
>http://sms.verimor.com.tr/v2/blacklists?username=908501234567&password=xxxx&offset=0&limit=100

**Cevap (Başarılı):**

**Total** değeri toplam kayıt sayısını verir, bir sorguda en fazla 100 adet kayıt dönülür. Devamını almak için offset değerini yükseltip tekrar sorgulamalısınız.
```json
HTTP/1.1 200 OK
{
  "total": 2,
  "records": [
    {
      "created_at" : "2019-11-25T11:13:24.988+03:00",
      "phone"      : "905444876543",
      "source"     : "ret_web"
    },
    {
      "created_at" : "2020-06-02T16:59:56.957+03:00",
      "phone"      : "905335876543",
      "source"     : "oim"
    }
  ]
}
```

**Cevap (Başarısız):**
```json
HTTP/1.1 401 Unauthorized
Geçersiz kullanıcı adı/şifre
```


---
**HTTP POST İLE KARALİSTENİZE NUMARA EKLENMESİ**
---
Aşağıdaki örnekte olduğu gibi URL çağrılır

**Örnek:**
>POST http://sms.verimor.com.tr/v2/blacklists?username=908501234567&password=xxxx&phones=905444876543,905335876543

**Cevap (Başarılı):**

```json
HTTP/1.1 200 OK
OK
```

**Cevap (Başarısız):**
```json
HTTP/1.1 400 Bad Request
Invalid phone number: 123456
```

---
**HTTP DELETE İLE KARALİSTENİZDEKİ NUMARANIN SİLİNMESİ**
---
Aşağıdaki örnekte olduğu gibi URL, DELETE metoduyla çağrılır

**Örnek:**
>DELETE http://sms.verimor.com.tr/v2/blacklists/905444876543,905335876543?username=908501234567&password=xxxx

**Cevap (Başarılı):**

```json
HTTP/1.1 200 OK
OK
```

**Cevap (Başarısız):**
```json
HTTP/1.1 400 Bad Request
Invalid phone number: 123456
```

----
**İYS İZNİ GÖNDERİMİ**
----

Ticari ileti göndermek için markalarınızı İYS'ye (İleti Yönetim Sistemi) kaydettirmiş olmalı ve [OİM Başlık Yönetiminden](https://oim.verimor.com.tr/headers) ilgili başlığa İYS kodlarını girmiş olmanız gerekir. Bu işlemleri yaptıktan sonra müşterilerinizden aldığınız izinleri aşağıdaki yöntemle İYS'ye bildirebilirsiniz.

**HTTP POST JSON ile İYS İzni Gönderimi** 

Aşağıdaki örnekte olduğu gibi bir JSON string POST edilir.

```json
POST http://sms.verimor.com.tr/v2/iys_consents.json
Host: sms.verimor.com.tr
Content-Type: application/json
Accept: */*
 
{
  "username"    : "908501234567",
  "password"    : "xxxxxxx",
  "source_addr" : "BASLIGIM",
  "consents": [
               { 
                "type"           : "MESAJ",
                "source"         : "HS_WEB",
                "status"         : "ONAY",
                "recipient_type" : "BIREYSEL",
                "consent_date"   : "2020-11-10 12:57:00",
                "recipient"      : "905311234567",
               },
               { 
                "type"           : "MESAJ",
                "source"         : "HS_MESAJ",
                "status"         : "RET",
                "recipient_type" : "BIREYSEL",
                "consent_date"   : "2020-11-10 13:30:30",
                "recipient"      : "905311234568",
               }
              ]
}
```
* username: Verimor hesabınızın kullanıcı adı. (zorunlu)
* password: API şifreniz. (zorunlu)
* source_addr: Gönderici kimliği (Başlık). Source_addr boş ise sistemde kayıtlı ilk başlığınız kullanılır.
* type: İletişim kanalı (zorunlu). Alabileceği değerler: "ARAMA" "MESAJ" "EPOSTA"
* source: İzini aldığınız kaynak (zorunlu). Alabileceği değerler: "HS_FIZIKSEL_ORTAM" "HS_ISLAK_IMZA" "HS_WEB" "HS_CAGRI_MERKEZI" "HS_SOSYAL_MEDYA" "HS_EPOSTA" "HS_MESAJ" "HS_MOBIL" "HS_EORTAM" "HS_ETKINLIK" "HS_2015" "HS_ATM" "HS_KARAR"
* status: İzin durumu (zorunlu). Alabileceği değerler: "ONAY" "RET"
* recipient_type: Alıcı türü (zorunlu). Alabileceği değerler: "BIREYSEL" "TACIR"
* source: İzin alınma tarihi (zorunlu).
* recipient: Alıcı telefon numarası (zorunlu).

**Cevap:**
```json
HTTP/1.1 200 OK
20212
```

----
**İYS GÜNLÜK RAPOR ALIMI**
----

Verimor, hergün İYS sisteminden gün sonunda, günlük izin hareketlerini inceleyip bir kampanya yaratır. Oluşturulan bu kampanya, [OİM üzerinden](https://oim.verimor.com.tr/sms_settings/edit) daha önce belirlediğiniz bir URL'ye (İYS Push URL) gönderilir. 

Aşağıdaki örnekte olduğu gibi bir JSON string POST edilir.

```json
POST http://sizin.adresiniz.com.tr/iys_campaign_push
Host: sizin.adresiniz.com.tr
Content-Type: application/json
Accept: */*
 {
  "iys_campaign_id"       : 1234,
  "report_date"        : "2021-01-18"
 }
```

Daha sonra bu id ile aşağıdaki şekilde sisteme gelip, izinler çekilebilir.


**Örnek:**
>http://sms.verimor.com.tr/v2/iys/campaigns/[IYS_CAMPAIGN_ID]/consents?username=908501234567&password=xxxx&offset=0&limit=100

**Cevap (Başarılı):**

**Total** değeri toplam kayıt sayısını verir, bir sorguda en fazla 100 adet kayıt dönülür. Devamını almak için offset değerini yükseltip tekrar sorgulamalısınız.
```json
HTTP/1.1 200 OK
{
  "total": 2,
  "records": [
      {
         "type":"MESAJ",
         "source":"HS_WEB",
         "recipient":"905331001727",
         "status":"ONAY",
         "consent_date":"2020-06-11T22:14:00.000+03:00",
         "recipient_type":"BIREYSEL"
      },
      {
         "type":"MESAJ",
         "source":"HS_WEB",
         "recipient":"905331001017",
         "status":"ONAY",
         "consent_date":"2020-06-11T22:14:00.000+03:00",
         "recipient_type":"BIREYSEL"
      }
  ]
}
```


----
**HATA KODLARI**
----
SMS gönderirken ve gönderim raporu alırken size dönen status sahalarında aşağıdaki tablodaki değerler olabilir:

<br/>

**Mesaj Gönderirken Dönebilecek Durumlar ve Açıklamaları**

| Web_Arayüzü_Durumları | API                         | Açıklama                                                                                |
| --------------------- |-----------------------------| ----------------------------------------------------------------------------------------|
| -                     | INVALID_SOURCE_ADDRESS      | Başlık kabul edilmedi.                                                                  |
| -                     | MISSING_MESSAGE             | Gönderilecek mesaj verilmemiş.                                                          |
| -                     | MESSAGE_TOO_LONG            | Mesaj çok uzun.                                                                         |
| -                     | INVALID_PERIOD              | Mesajın geçerlilik süresi (validity period) geçersiz. (1dk. ile 48 saat arasında değil).|
| -                     | INVALID_DELIVERY_TIME       | "schedule_delivery_time" parametresi geçersiz veya geçmiş tarihe ait.                   |
| -                     | INVALID_DATACODING          | datacoding parametresi hatalı verilmiş.                                                 |
| -                     | MISSING_IYS_BRAND_CODE      | Ticari gönderimlerde başlığın marka kodunun tanımlanmış olması gereklidir               |
| -                     | MISSING_DESTINATION_ADDRESS | Mesaj için alıcı verilmemiş.                                                            |
| Hatalı Numara         | INVALID_DESTINATION_ADDRESS | Alıcı telefon numarasının formatı geçersiz. (905121234567 gibi olmalı)                  |
| Kredi Yetersiz        | INSUFFICIENT_CREDITS        | Mesajı göndermek için yeterli bakiyeniz yok.                                            |
| Yasaklı içerik        | FORBIDDEN_MESSAGE           | Mesajınız yasak kelime(ler) içeriyor.                                                   |

<br/>

**Mesaj Durumu Alınırken Dönebilecek Durumlar ve Açıklamaları**

| Web_Arayüzü_Durumları | API_Durumları                   | Açıklama                                                                                                                    |
| --------------------- | --------------------------------| ----------------------------------------------------------------------------------------------------------------------------|
| Gönderiliyor          | SENDING                         | Mesaj gönderiliyor.                                                                                                         |
| Bekliyor              | WAITING                         | Mesaj gönderildi. Cevap bekleniyor.                                                                                         |
| İletildi              | DELIVERED                       | Mesaj iletildi.                                                                                                             |
| İletildi              | SENT                            | Mesaj iletildi. Fakat operatör gönderim raporunu desteklemediği için teyit edilemiyor. (Uluslararası bazı yönlerde oluşur.) |
| İletilemedi           | NOT_DELIVERED                   | Mesaj iletilemedi. (Genelde alıcı numaranın aktif olmamasından kaynaklanır.)                                                |
| Zaman aşımı           | EXPIRED                         | Zaman aşımı. Mesajınız belirlediğiniz geçerlilik süresi içinde alıcısına teslim edilemedi.                                  |
| Hatalı Numara         | INVALID_DESTINATION_ADDRESS     | Alıcı telefon numarası geçersiz. (Hiçbir operatöre kayıtlı değil.)                                                          |
| Reddedildi            | REJECTED                        | Mesajınızın gönderimi reddedildi. (Genelde gsm operatörü tarafından içerik kontrolü sonucu oluşur.)                         |
| Mükerrer Gönderim     | DOUBLE_SEND_ERROR               | Aynı içerik aynı gün aynı başlıkla aynı numaraya gönderilmiş. Mükerrer gönderim engellendi.                                 |
| Karalistede           | BLACKLISTED_DESTINATION_ADDRESS | Alıcı kara listenizde.                                                                                                      |
| Tarife Bulunamadı     | MISSING_TARIFF                  | Alıcının operatörü tarifelerimiz arasında bulunamamıştır. (Uluslararası yönlerde oluşur.)                                   |
| Geçersiz Şebeke       | ROUTE_NOT_AVAILABLE             | Hesabınız bu alıcıya mesaj gönderemez. (Uluslararası bazı yönlerde oluşur.)                                                 |
| Geçersiz Şebeke       | NETWORK_NOTCOVERED              | Hesabınız bu alıcıya mesaj gönderemez. (Uluslararası bazı yönlerde oluşur.)                                                 |
| Gönderim Hatası       | SEND_ERROR                      | Mesajınız gönderilirken hata oluştu. (Sebebi çeşitli olabilir.)                                                             |
| Uluslararası Gönderim Kapalı | INTERNATIONAL_DENIED     | OİM'de SMS ayarlarından 'uluslararası gönderim' ayarı kapalı olduğu için gönderilmedi.                                      |

<br/>

**Mesaj Hata Kodları (gsm_error)**  
İletilemeyen mesajlar için karşı operatörden alınan teknik hata kodları ve açıklamaları aşağıda verilmiştir.

| Hata No | Hata Kodu             | Açıklama                                       |
|---------|-----------------------|------------------------------------------------|
| 1       | EC_UNKNOWN_SUBSCRIBER | Numara karşı operatörün veritabanında bir aboneye tanımlı değil |
| 6       | EC_ABSENT_SUBSCRIBER_SM | Karşı aboneden sinyal alınamadı. Abonenin telefonunun kapalı olduğu durumda veya sinyalin zayıf olduğu durumda görülür |
| 11      | EC_TELESERVICE_NOT_PROVISIONED | Karşı abonenin mobil hizmeti operatörü tarafından durduruldu |
| 13      | EC_CALL_BARRED | Karşı abone 'Rahatsız Etme' (DND) hizmetini açtı, hiç mesaj almamayı tercih etti |
| 27      | EC_ABSENT_SUBSCRIBER | Karşı abone çevrimiçi değil, telefon cihazı tarafından teyit edildi. Telefon kapatılınca görülür. |
| 31      | EC_SUBSCRIBER_BUSY_FOR_MT_SMS | Karşı operatör fazla trafikten dolayı meşgul olduğunu bildirdi |
| 32      | EC_SM_DELIVERY_FAILURE | Karşı operatör kısa mesajı abonesine iletemediğini bildirdi |
| 34      | EC_SYSTEM_FAILURE | Karşı operatör sistem hatası bildirdi |
| 256     | EC_SM_DF_MEMORYCAPACITYEXCEEDED | Karşı abonenin telefon cihazında mesajı kaydedecek yer kalmadı |
| 257     | EC_SM_DF_EQUIPMENTPROTOCOLERROR | Karşı operatör, abonenin telefon cihazında hata olduğunu bildirdi |
| 258     | EC_SM_DF_EQUIPMENTNOTSM_EQUIPPED | Karşı operatör, abonenin telefon cihazında hata olduğunu bildirdi |
| 500     | EC_PROVIDER_GENERAL_ERROR | Karşı operatör genel hata bildirdi |
| 502     | EC_NO_RESPONSE | Mesaj karşı operatöre iletildi fakat olumlu veya olumsuz bir iletim raporu dönmedi |
| 1030    | EC_OR_POTENTIALVERSIONINCOMPATIBILITY | Karşı operatör genel hata bildirdi |
| 1155    | EC_NNR_SUBSYSTEMFAILURE | Karşı operatör, sistem hatasından dolayı abonesine ulaşamadığını bildirdi |
| 1157    | EC_NNR_MTPFAILURE | Karşı operatör genel hata bildirdi |
| 1281    | EC_UA_USERSPECIFICREASON | Karşı operatör genel hata bildirdi |
| 1536    | EC_PA_PROVIDERMALFUNCTION | Karşı operatör genel hata bildirdi |
| 2048    | EC_TIME_OUT | Mesaj karşı operatöre geçerlilik süresi içinde iletilemedi |
| 2049    | EC_IMSI_BLACKLISTED | Karşı abonenin SIM kartı operatörünün karalistesinde |
| 2050    | EC_DEST_ADDRESS_BLACKLISTED | Numara karalistemizde olduğu için iletilemedi |
| 2051    | EC_INVALIDMSCADDRESS | Mesaj metni karalistemizde olduğu için iletilemedi |
| 2053    | EC_BLACKLISTED_SENDERADDRESS | Mesaj başlığının kullanımı için ek onay alınması gerekli |
| 4100    | EC_MESSAGE_CANCELED | Karşı operatör mesajı abonesine geçerlilik süresi içinde iletemedi |
| 4101    | EC_VALIDITYEXPIRED | Karşı operatör mesajı abonesine geçerlilik süresi içinde iletemedi |
| 4103    | EC_DESTINATION_FLOODING | Karşıdaki abone çok fazla mesaj almış olduğu için yeni mesaj kabul etmiyor |
| 4104    | EC_DESTINATION_TXT_FLOODING | Karşıdaki aboneye aynı mesaj çok defa gönderilmiş olduğu için yeni mesaj kabul etmiyor |

----
**SMS BOY KARAKTER LİMİTLERİ**
----

|      | Normal (datacoding=0) | Türkçe (datacoding=1) | Unicode (datacoding=2) |
|------|-----------------------|-----------------------| -----------------------|
|1 boy |0-160                  | 0-155                 | 0-70                   |
|2 boy |161-306                | 156-298               | 71-134                 |
|3 boy |307-459                | 299-447               | 135-201                |
|4 boy |460-612                | 448-596               | 202-268                |
|5 boy |613-765                | 597-745               | 269-335                |
|6 boy |766-918                | 746-894               | 336-402                |
|7 boy |919-1071               | 895-1043              | 403-469                |

**Not-1:** datacoding=0 veya datacoding=1 gönderimlerde aşağıdaki karakterler 2 karakter sayılır.
^ { } \ [ ] ~ | € <br/>
**Not-2:** Sadece (Ş ş Ğ ğ ç ı İ) harfleri Türkçe olarak kabul edilir ve datacoding=1 olarak gönderilmelidir. Diğer Türkçe karakterleri (Ö ö U ü Ç) datacoding=0 olarak gönderebilirsiniz. <br/>
**Not-3:** HTTPS olarak API’mizi kullanırken SSL bağlanıtısı için kullandığınız kütüphane sisteminizde kök sertifikalar yüklü olmadığından sertifikamızı doğrulamayabilir. Bu sorunu çözmek için rapidssl.crt kök sertifika dosyasını [buraya](https://github.com/verimor/SMS-API/blob/master/rapidssl.crt) tıklayarak indirip sisteminize kurmalısınız.<br/>
**Not-4:** API ile dakikada 150 sms paketi (request) gönderebilirsiniz. 1 paket 10 MB büyüklüğünü geçemez. Bu limitler içinde, paketin yapısına bağlı olmakla birlikte dakikada 100.000.000 mesaja kadar gönderebilirsiniz. Paket boyutu limitini aştığınızda 413 (Request Entity Too Large) hatası döner. Request limitini aştığınızda 429 (Too Many Requests) hatası döner.<br/>
**Not-5:** API ile Gönderim raporu alabilme request limiti dakikada 20 adettir. Request limitini aştığınızda 429 (Too Many Requests) hatası döner.
