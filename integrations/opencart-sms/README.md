# Opencart – Verimor SMS Eklentisi ile Neler Yapabilirsiniz?**

[Verimor](https://www.verimor.com.tr/) aboneliğinizle, Opencart müşterilerinize SMS gönderebileceğiniz bu modül ile yeni müşteri oluştuğunda, yeni bir sipariş verildiğinde veya sipariş durumu güncellendiğinde ilgili numaraya istenen içerikte kısa mesaj gönderilmesi sağlanmaktadır.

## Özellikler
- SMS Ayarları
  - Sisteme yeni üye olan bir müşteri olduğunda virgül ile ayrılmış belirlenen numaralara istenen içerikte sms gönderebilirsiniz.
  - Sisteme yeni üye olan bir müşteriye istenen içerikte sms gönderebilirsiniz.
  - Sisteme yeni sipariş bir sipariş geldiğinde virgül ile ayrılmış belirlenen numaralara istenen içerikte SMS gönderebilirsiniz.
  - Yeni bir sipariş geldiğinde sipariş sahibine istenen içerikte SMS gönderebilirsiniz.
  - Verilen siparişin sipariş durumu güncellendiğinde her durum için ayrı ayrı SMS içeriği belirleyip bu SMS’lerin müşteriye gitmesini sağlayabilirsiniz.
  - Sipariş müşteri tarafından iptal/iade edildiğinde belirlenen numaralara istenilen içerikte SMS gönderilmesi sağlayabilirsiniz.

- Özel SMS
  - Virgül ile ayrılmış numaralara toplu SMS gönderimi sağlayabilirsiniz.

- Toplu SMS
  - Siparişler sayfasındaki istenilen siparişler seçilerek istenilen içerikte SMS gönderimi yapılabilirsiniz.
  - Müşteriler sayfasında istenilen müşteriler seçilerek istenilen içerikte SMS gönderimi yapabilirsiniz.

- Gelen SMS
  - Numaralarınıza gelen smsleri görüntüleyip müşterileriniz ile eşleştirebilir ve istediğiniz mesaja modül üzerinden cevap verebilirsiniz.

- Bulut Santral
  - Bulut Santralinize gelen veya giden çağrıları görüntülüyebilir. - Opencart sisteminizdeki müşterileriniz ile direk eşleştirebilirsiniz. İstenilen çağrının detayına tıklayarak gidebilirsiniz.

## Opencart SMS Entegrasyonu
- Kurulum
  - Modülü opencart sürümünüzü dikkate alarak [bu adres](https://www.opencart.com/index.php?route=marketplace/extension/info&extension_id=46310) üzerinden indirin.
  - İndirilen ocmod zip dosyasını Extensions > Installer adresinden yükleyin.
  - Extensions > Extensions > Modules altından Verimor SMS modülünü kurun.
  - Extensions > Modifications sayfaındaki refresh (yenileme) butonunu tıklayın.
  - Extensions menüsü altına Verimor menüsü eklenmiş olacaktır. Bu menüye tıklayarak modül ayarlarına gidebilirsiniz.
  - OIM üzerinden kimlik bilgilerini alma.
    - SMS Hizmeti > SMS Ayarlarım sayfasından API Ayarlarım altında erişimi açmalı ve kullanıcı adı ve şifre alanını kopyalayın. 
    - Opencart sisteminin çalıştığı sunucunuzun IP adresini Api İzinli IP’ler alanına eklemelisiniz.
    - Bulut Santral Hizmeti > Santral Ayarlarım sayfasında CRM Entegrasyonu altında API anahtarınızı kopyalayın.
  - Opencart sisteminizden Verimor SMS modülüne girin Kullanıcı adı, şifre ve API Key alanına daha önce kopyaladığınız verileri yapıştırın.
  - Hesabımı Doğrula butonuna tıklayın. Herhangi bir sorun yaşamazsanız sağ üste kalan kredinizi görebileceksiniz. Hata açıklamaları için github sayfamıza bakabilirsiniz.
## Dikkat
- Bu modül Opencart sürümlerinin saf versiyonları ile test edilmiştir. (Güncelleme yapılan versiyonlar test edilmemiştir.)
- Bu modül Opencart sisteminde extra başka bir modül olmadan test edilmiştir.