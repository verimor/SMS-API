# ZOHO CRM – Verimor SMS Entegrasyonu

Bu eklenti ile Zoho CRM üzerinde bulunan kişi ve potansiyel müşterilere Verimor üzerinden tekil veya toplu SMS gönderebilir bu kişi veya firmaları IYS sisteminize gönderebilirsiniz.. Eklenti Zoho Marketplace'e özel plugin olarak yüklenir.

Modül adresi: ([Verimor SMS For ZOHO CRM](https://crm.zoho.com/market/installPrivatePlugin.do?portalName=verimortelekom&nameSpace=verimorsms&versionIndex=4576373000000179001))

## ZOHO CRM Nedir?

Zoho CRM, işletmelerin satış, pazarlama ve müşteri destek süreçlerini tek bir platformda yönetmesini sağlayan bulut tabanlı bir CRM uygulamasıdır.

## Özellikler
- Tekil veya toplu SMS gönderimi: Zoho'daki kişiler ve potansiyel müşterilere (leads/contacts/account) SMS gönderebilirsiniz.
- Manuel numara ekleyerek gönderim sağlayabilirsiniz.
- OIM üzerinden eklediğiniz hazır mesajları seçip gönderebilirsiniz.
- SMS bakiyenizi görüntüleyebilirsiniz.
- IYS modülüne sahipseniz ticari gönderim sağlayabilirsiniz.
- Alıcıları topluca IYS ye yükleyebilirsiniz.
- Alıcı detay sayfalarında (contact/account) IYS izinlerini sorguluyabilir ve değiştirebilirsiniz.
- Uzantı durumunu kontrol edebilirsiniz.

## Kurulum
1. OIM içerisinde ([SMS Ayarlarım](https://oim.verimor.com.tr/sms_settings/edit)) ekranında
   - Api erişimini açın.
   - Api izinli IP ler alanına 135.84.80.0/24 adresini ekleyin.
   - Buradaki Kullanıcı ad ve API şifresini kopyalayın.
2. Verilen modül linkini açın ve Zoho Marketplace üzerinden eklentiyi yükleyin:
   ([Verimor SMS For ZOHO CRM](https://crm.zoho.com/market/installPrivatePlugin.do?portalName=verimortelekom&nameSpace=verimorsms&versionIndex=4576373000000179001))
3. Yükleme tamamlandıktan sonra Zoho içinde eklenti ayar formu açılacaktır.
4. Ayar formunda girilmesi gereken bilgiler:
   - SMS Ayarlarım dan kopyaladığınız yetkilendirilmiş API kullanıcı ve şifresi girin
   + <img src="setting.png" width="500" >
5. Bilgilerinizi girdikten sonra aşağıdaki ayarlar ekranı açılacaktır. Buradan "kontrol et" butonları ile kurulumun başarılı olduğunu veya hataları görebilirsiniz.
   + <img src="extension-setting.png" width="500" >

> Not: Eklenti kurulumunda Zoho tarafında gerekli izinleri vermeniz istenecektir; izinleri onaylayın.

## Kullanım
- Verimor SMS (Toplu) butonu: Seçilen birden fazla veriye SMS gönderimini sağlar
   + <img src="batch.png" width="500" >
- Verimor SMS butonu: Detayı incelenen veriye SMS gönderimini sağlar.
   + <img src="sms-send.png" width="500" >
- Verimor IYS sekmesi, detayına bakılan alıcının IYS izinlerini görebilir ve değiştirebilirsiniz. (Tüm izinler HS_WEB türü ile gönderilecektir.)
  + <img src="iys-detail.png" width="500" >
- Verimor IYS butonu seçilen alıcıları toplu bir şekilde IYS gönderebilirsiniz.
  + <img src="iys-batch.png" width="500" >

## Örnek İş Akışı
1. Zoho içinde Lead Contact veya Account listesine gidin
2. SMS göndermek istediğiniz verileri seçin
3. Verimor SMS butonlarından birini seçin ve SMS gönderimi ekranını açın.
4. Gönderici başlığı seçin. Ticari gönderim için IYS başlığınız ile ilişkili başlığı seçmelisiniz
5. Gönderim Tipini seçin
6. SMS içeriğini girin ve gönderin.

## Destek
Kurulum veya gönderim sırasında sorun yaşarsanız lütfen Verimor destek ekibi ile iletişime geçin.