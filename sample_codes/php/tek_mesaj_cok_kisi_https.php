//SMS API ile tek mesajın çok kişiye gönderimi örneği (HTTPS)
<?php function sendSMS($header, $message, $phones){ $sms_msg = array( "username" => "xxxx",
    "password" => "xxxx",
    "source_addr" => $header,
//    "valid_for" => "48:00",
//    "send_at" => "2015-02-20 16:06:00",
//    "datacoding" => "0",
    "custom_id" => "1424441160.9331344",
    "messages" => array(
    array(
     "msg" => $message,
     "dest" => $phones
   )
  )
 );
    $ch = curl_init('https://sms.verimor.com.tr/v2/send.json');
    curl_setopt_array($ch, array(
        CURLOPT_POST => TRUE,
        CURLOPT_RETURNTRANSFER => TRUE,
        CURLOPT_HTTPHEADER => array('Content-Type: application/json'),
        CURLOPT_POSTFIELDS => json_encode($sms_msg),
        CURLOPT_CAINFO => "/sunucunuzda/bir/yol/rapidssl.crt" // bu dosyayi https://www.verimor.com.tr/makaleler/sms-api-dokumani/ sayfamizdan indirin
    ));
    $http_response = curl_exec($ch);
    $http_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
    
    if ($http_code == 200){
      return $http_response;
    }
 
    if($http_code == 0){
      echo "HTTPS bağlantısı kurulamadı. Servis adresinin doğru olduğunu, php'nin network bağlantısı kurabildiğini ve sertifikalarınızın güncel olduğunu teyit edin.\n";
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
}
 
$source_addr = "BASLIGIM";
$message = "Test mesajıdır";
$dest = "905321234567,905321234568";
 
$campaign_id = sendSMS($source_addr, $message, $dest);
if($campaign_id === false)
  echo "Mesaj gonderme basarisiz.\n";
else
  echo "Mesaj basariyla gonderildi. Kampanya ID'si: $campaign_id\n";
 
?>
