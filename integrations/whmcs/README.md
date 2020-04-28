**WHMCS – Verimor SMS Eklentisi ile Neler Yapabilirsiniz?**
* Toplu olarak ve kişi bazlı SMS gönderebilirsiniz.
* Eklentide toplam 25 adet SMS şablonu bulunmaktadır. Tüm şablonlar ön tanımlı olarak gelir ve aktifleştirmeniz yeterlidir.
* SMS şablonlarını güncelleyebilirsiniz. Böylelikle müşterileriniz için özelleştirilmiş SMS’ler oluşturabilirsiniz.
* Müşteri özet sayfasında bulunan “Sms Gönder” kutucuğu ile eklenti paneline girmeden SMS gönderebilirsiniz.

**Kurulum ve Kullanım**
1. Online İşlem Merkezi (OİM) > SMS Hizmeti > SMS Ayarlarım sayfasındaki “API Erişimi” pasif ise aktif hale getiriniz.
2. Eklenti dosyasını WHMCS sisteminizde bulunan /whmcs/modules/addons/ klasörüne atınız. (Eklenti dosyasını indirmek için [tıklayınız](https://github.com/verimor/SMS-API/raw/master/integrations/whmcs/verimor.zip).)
3. WHMCS admin panelinizden Kurulum->Eklenti Modülü’nden tüm eklentileri listeleyiniz.
4. VERİMOR SMS eklentisini listeden bularak aktifleştirerek eklenti izinleri ayarlayınız.
5. Artık “Eklentiler” sekmesinde “VERİMOR SMS” görüntülenecektir.

**Şablonlar**
* Sipariş Onay: Sipariş onaylandığında müşteriye SMS gönderir.
* Yönetici Giriş: Yönetici girişi yapıldığında belirlenmiş numaralara SMS gönderir.
* Paket Değişikliği: Modül paket değişikliğinde mesaj gönderir.
* Modül Şifresi Değişimi: Hosting hesabı şifresi değiştiğinde SMS gönderir.
* Yeni Hosting Hesabı: Bir hizmet aktifleştirildiğinde SMS göderir
* Bir Hizmet Durdurma: Bir hizmet duraklatıldığında SMS gönderir.
* Hizmet Başlatma: Bir hizmet tekrar başlatıldığında SMS gönderir.
* Alan Adı Kayıt:
* * Bir domain kayıt edildikten sonra müşteriye SMS gönderir.
* * Bir domain kayıt edildikten sonra yöneticiye SMS gönderir.
* Alan Adı Kayıt Hatası:
* * Domain kayıt edilirken hata oluşursa müşteriye SMS gönderir.
* * Domain kayıt edilirken hata oluşursa yöneticiye SMS gönderilir.
* Alan Adı Yenileme:
* * Domain yenilendikten sonra müşteriye SMS gönderir.
* * Domain yenilendikten sonra yöneticiye SMS gönderir.
* Alan Adı Yenileme Hata: Alan adı yenilemede hata olduğunda yöneticiye mesaj gönderir.
* Yeni Müşteri:
* * Müşteri kaydı tamamlandıktan sonra müşteriye SMS gönderir.
* * Müşteri kaydı tamamlandıktan sonra yöneticiye SMS gönderir.
* Müşteri Şifre Değişimi: Müşteri şifresini değiştirdiğinde müşteriye SMS gönderir.
* Müşteri Girişi: Müşteri sisteme giriş yaptıktan sonra belirlenmiş yöneticiye mesaj gönderir.
* Alan Adı Kalan Süre Bildirimi: Alan adı süresinin dolmasına [x] gün kala mesaj gönderir. [x] değeri kurulumda 15 olarak atanır.
* Yeni Fatura: Yeni bir fatura oluşturulduğunda müşteriye SMS gönderir.
* Ödenmiş Fatura: Fatura ödendiğinde müşteriye SMS gönderir.
* Fatura Hatırlatma: Ödenmemiş fatura için hatırlatma mesajı gönderir.
* Ödenmemiş Fatura 1. Hatırlatma: Ödenmemiş faturanın ilk zaman aşımında SMS gönderir.
* Ödenmemiş Fatura 2. Hatırlatma: Ödenmemiş faturanın ikinci zaman aşımında SMS gönderir.
* Ödenmemiş Fatura 3. Hatırlatma: Ödenmemiş faturanın üçüncü zaman aşımında SMS gönderir.
* Yeni Ticket Açılması: Bir ticket açıldığında yöneticiye SMS gönderir.
* Ticket Cevabı:
* * Bir ticket cevaplandığında müşteriye SMS gönderir.
* * Bir ticket cevaplandığında yöneticiye SMS gönderir.
* Ticket Kapatılması: Ticket kapatıldığında müşteriye SMS gönderir.
* Müşteri Girişinde OTP: Müşteri girişi sırasında OTP SMS göndererek telefon numarası doğrulaması istenir.
* Yeni Üye OTP: Yeni bir üyelik oluşturulduğunda, üyeye OTP sms göndererek üyenin ana sayfasında telefon numarası doğrulması istenir.