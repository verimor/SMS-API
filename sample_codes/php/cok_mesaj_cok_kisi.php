<?php
// Sunucu IP adresinizi https://oim.verimor.com.tr/sms_settings/edit sayfasından girmiş olmanız gerekir.
// Girmezseniz 401 hatası alırsınız.

// SMS API ile cok mesajın çok kişiye gönderimi örneği (HTTP)
// Aşağıdaki örnek kodu kendinize özelleştirerek kullanabilirsiniz.

function sendSMS($source_addr, $messages){
  $campaign = array(
    "username" => "xxxx", // https://oim.verimor.com.tr/sms_settings/edit adresinden öğrenebilirsiniz.
    "password" => "xxxx", // https://oim.verimor.com.tr/sms_settings/edit adresinden belirlemeniz gerekir.
    "source_addr" => $source_addr, // Gönderici başlığı, https://oim.verimor.com.tr/headers adresinde onaylanmış olmalı, değilse 400 hatası alırsınız.
//    "valid_for" => "48:00",
//    "send_at" => "2015-02-20 16:06:00",
//    "datacoding" => "0",
    "custom_id" => "1424441160.9331344",
    "messages" => $messages
  );

  $ch = curl_init('http://sms.verimor.com.tr/v2/send.json');
  curl_setopt_array($ch, array(
      CURLOPT_POST => TRUE,
      CURLOPT_RETURNTRANSFER => TRUE,
      CURLOPT_HTTPHEADER => array('Content-Type: application/json'),
      CURLOPT_POSTFIELDS => json_encode($campaign),
  ));

  $http_response = curl_exec($ch);
  $http_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
  if ($http_code == 200){
      return $http_response;
    }

    if($http_code == 0){
      echo "HTTP bağlantısı kurulamadı. Servis adresinin doğru olduğuna ve php'nin network bağlantısı kurabildiğine emin olun.\n";
      echo "$http_code $http_response\n";
      echo curl_error($ch)."\n";
      print_r( error_get_last());
      return false;
    }

    if($http_code != 200){
      echo "$http_code $http_response\n";
      echo curl_error($ch)."\n";
      print_r( error_get_last());
      return false;
    }

  return $http_response;
}

$source_addr = "BASLIGIM";
$messages = array();

$messages[] = array(
  "msg" => "Sayın Mehmet Öztürk. Doğum gününüzü kutlar, nice yıllarda beraber çalışmayı dileriz.", // Bu metin UTF8 olmalı, değilse 400 hatası alırsınız. Veritabanından alınan string'ler, veritabanı bağlantısının encoding'iyle gelir, UTF8 değilse çevirmeniz gerekir.
  "dest" => "905321234567"
);

$messages[] = array(
  "msg" => "Sayın abonemiz. Gece 12:00'da bakım çalışması yapılacak olup kısa süreli bir hizmet kesintisi yaşanacaktır. Bilginize sunarız.",
  "dest" => "905322345678,905323456789,905325678901"
);

$campaign_id = sendSMS($source_addr, $messages);
if($campaign_id === false)
  echo "Mesaj gonderme basarisiz.\n";
else
  echo "Mesaj basariyla gonderildi. Kampanya ID'si: $campaign_id\n";

?>
