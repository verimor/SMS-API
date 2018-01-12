**SMS API Dökümanı**
----

Bu doküman, Verimor SMS API (kısaca Smsapi) ile mesaj gönderiminin nasıl yapılacağını, gönderim raporunun ve gelen smslerinizin nasıl alınacağını anlatır.

Smsapi ile sms göndermek için iki bilgiye ihtiyaç vardır: <br/>
1- Verimor hesabınızın kullanıcı adı (12 haneli telefon numaranız, 908501234567 gibi) <br/>
2- API şifreniz ( [OİM üzerinden](https://oim.verimor.com.tr) tanımlayabilirsiniz. Aynı zamanda bu menüden API erişiminizi sadece belirli IP adresine kısıtlayarak hesap güvenliğinizi de arttırabilirsiniz.

**SMS GÖNDERİMİ**

Smsapi gönderim için iki yöntemi destekler. Bunlar **HTTP(S) GET** (Plain de denir) ve **HTTP(S) POST JSON**’dır. İkisi de cevabını düz metin olarak döndürür.

**HTTP GET ile SMS Gönderimi**

Aşağıdaki örnekte olduğu gibi bir URL çağırılır.

**Örnek:**
> http://sms.verimor.com.tr/v2/send?username=908501234567&password=xxxxxxx&source_addr=BASLIGIM&msg=deneme12&dest=905311234567,905319876543&datacoding=0&valid_for=2:00

* username: Verimor hesabınızın kullanıcı adı. (zorunlu)
* password: API şifreniz. (zorunlu)
* source_addr: Gönderici kimliği (Başlık). Source_addr boş ise sistemde kayıtlı ilk başlığınız kullanılır.
* msg: Gönderilecek mesaj. Türkçe harf içerebilir. Maksimum uzunluğu Türkçe harf içeriyorsa 1043, içermiyorsa 1071 karakter’dir. (zorunlu). Encoding her zaman UTF8 beklenir.
* dest: Mesajın gönderileceği telefon numaraları. Birden fazla numara varsa virgül ile ayrılmalıdır. Telefon numaraları 90 ile başlayıp 12 hane olmalıdır. (zorunlu)
* valid_for: Mesajın geçerlilik süresi. SS:DD (veya S:DD) formatında olmalı. (Varsayılan değer 24:00, Minumum değer 00:01, Maksimum değer 48:00)
* datacoding: Mesaj metni için kullanılacak karakter kodlaması. 0, 1 ve 2 değerlerini alabilir. Mesajda kullanılabilecek harfleri ve mesajın boy limitlerini belirler. Boş ise mesaj metnine bakılır, türkçe harf varsa 1, yoksa 0 kaydedilir. Mesaj boyları tablosu için dokümanın sonuna bakınız.

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
                "dest": "905311234567,905319876543"
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
* dest: Mesajın gönderileceği telefon numaraları. Birden fazla numara varsa virgül ile ayrılmalıdır. Telefonların formatı: 905xxxxxxxxx şeklinde olmalıdır. (zorunlu)
* datacoding: Mesaj metni için kullanılacak karakter kodlaması. 0, 1 ve 2 değerlerini alabilir. Mesajda kullanılabilecek harfleri ve mesajın boy limitlerini belirler. Boş ise mesaj metnine bakılır, türkçe harf varsa 1, yoksa 0 kaydedilir. Mesaj boyları tablosu için dokümanın sonuna bakınız.

**Cevap:**
```json
HTTP/1.1 200 OK
20212
```
----
**GÖNDERİM RAPORU ALIMI**
----
Smsapi gönderim raporlarını iki şekilde teslim eder. Bunlar PUSH ve GET yöntemleridir.

**PUSH ile Gönderim Raporu Alımı**

Push yönteminde mesajın durumu ile ilgili bilgi (teslim edildi, zaman aşımı, numara hatalı vb.) alınır alınmaz [OİM üzerinden](https://oim.verimor.com.tr) daha önce belirlediğiniz bir URL tetiklenir. Aşağıdaki gibi bir JSON POST edilir:
```json
POST http://sizin.adresiniz.com.tr/sms_push
Host: sizin.adresiniz.com.tr
Content-Type: application/json
Accept: */*
 
[
 {
  "campaign_id"              : 20121,
  "direction"                : "outbound",
  "campaign_custom_id"       : "123456789",
  "message_id"               : "13582302",
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
  "direction"                : "outbound",
  "campaign_custom_id"       : "123456789",
  "message_id"               : "13582303",
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
* direction: Mesajın yönüdür. Gönderilen sms olduğu için outbound
* campaign_custom_id: Mesajın kampanyasına sizin tarafınızdan verilmiş özel ID.
* message_id: Mesaja API tarafından verilmiş ID.
* dest: Mesajın gönderildiği telefon numarası.
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
  "direction"                : "outbound",
  "campaign_custom_id"       : "123456789",
  "message_id"               : "13582302",
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
  "direction"                : "outbound",
  "campaign_custom_id"       : "123456789",
  "message_id"               : "13582303",
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
**GELEN SMS ALIMI**<br/>
Smsapi hesabınıza gelen sms’leri iki farklı yöntemle teslim edebilir. Bunlar PUSH ve GET yöntemleridir.

**PUSH ile Gelen SMS Alımı** <br/>
Push yönteminde hesabınıza bir mesaj gelir gelmez [OİM üzerinden](https://oim.verimor.com.tr) daha önce belirlediğiniz bir URL tetiklenir. Aşağıdaki gibi bir JSON POST edilir:
```json
POST http://sizin.adresiniz.com.tr/sms_push
Host: sizin.adresiniz.com.tr
Content-Type: application/json
Accept: */*
 
[
 {
  "message_id"       : 1234,
  "direction"        : "inbound",
  "received_at"      : "2017-01-01 09:00:00",
  "network"          : "TURKCELL",
  "source_addr"      : "905319876543",
  "destination_addr" : "4609",
  "keyword"          : "verimor",
  "content"          : "verimor deneme"
 }
]
```
* direction: Mesajın yönüdür. Gelen sms olduğu için inbound
* received_at: Mesajın alındığı tarih saat
* network: Mesajı gönderen operatör. TURKCELL, AVEA, VODAFONE-TR değerleri olabilir.
* source_addr: Mesajı gönderen numara
* destination_addr: Mesajın gönderildiği numara (Verimor abone numarası veya 4 haneli Verimor ücretsiz kısa numarası)
* keyword: Ortak kullanımlı kısa numaralardaki ayırt edici anahtar kelime
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
  "direction"        : "inbound",
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
  "direction"        : "inbound",
  "received_at"      : "2017-01-01T10:00:00.000+03:00",
  "network"          : "VODAFONE-TR",
  "source_addr"      : "905444876543",
  "destination_addr" : "4609",
  "keyword"          : "verimor",
  "content"          : "verimor test"
 }
]
```
**Cevap (Hiç mesajı olmadığı durumda):**
```json
HTTP/1.1 200 OK
[]
```
----

**DURUM MESAJLARI**
SMS gönderirken ve gönderim raporu alırken size dönen status sahalarında aşağıdaki tablodaki değerler olabilir:

**Mesaj Gönderirken Dönebilecek Durumlar ve Açıklamaları**

| Web_Arayüzü_Durumları | API                         | Açıklama                                                                                |
| --------------------- |-----------------------------| ----------------------------------------------------------------------------------------|
| -                     | INVALID_SOURCE_ADDRESS      | Başlık kabul edilmedi.                                                                  |
| -                     | MISSING_MESSAGE             | Gönderilecek mesaj verilmemiş.                                                          |
| -                     | MESSAGE_TOO_LONG            | Mesaj çok uzun.                                                                         |
| -                     | INVALID_PERIOD              | Mesajın geçerlilik süresi (validity period) geçersiz. (1dk. ile 48 saat arasında değil).|
| -                     | INVALID_DELIVERY_TIME       | "schedule_delivery_time" parametresi geçersiz veya geçmiş tarihe ait.                   |
| -                     | INVALID_DATACODING          | datacoding parametresi hatalı verilmiş.                                                 |
| -                     | MISSING_DESTINATION_ADDRESS | Mesaj için alıcı verilmemiş.                                                            |
| Hatalı Numara         | INVALID_DESTINATION_ADDRESS | Alıcı telefon numarasının formatı geçersiz. (905121234567 gibi olmalı)                  |
| Kredi Yetersiz        | INSUFFICIENT_CREDITS        | Mesajı göndermek için yeterli bakiyeniz yok.                                            |

**Mesaj Durumu Alınırken Dönebilecek Durumlar ve Açıklamaları**

| Web_Arayüzü_Durumları | API_Durumları                   | Açıklama                                                                                                                    |
| --------------------- |---------------------------------| ----------------------------------------------------------------------------------------------------------------------------|
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
| Mükerrer Gönderim     | INTERNATIONAL_DENIED            | OİM'de SMS ayarlarından 'uluslararası gönderim' ayarı kapalı olduğu için gönderilmedi.                                      |

**SMS Boy Karakter Limitleri**

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
**Not-4:** API ile saniyede 1 sms paketi (request) gönderebilirsiniz. 1 paket 10 MB büyüklüğünü geçemez. Bu limitler içinde, paketin yapısına bağlı olmakla birlikte saniyede 800.000 mesaja kadar gönderebilirsiniz. Paket boyutu limitini aştığınızda 413 (Request Entity Too Large) hatası döner. Request limitini aştığınızda 429 (Too Many Requests) hatası döner.

---
**HTTP GET ile SMS BAŞLIKLARIM Listesi Alımı**
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
