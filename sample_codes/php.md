**SMS API ile tek mesajın çok kişiye gönderimi örneği (php, http, https)**
----
Aşağıdaki örnek kodu kendinize özelleştirerek kullanabilirsiniz.
```php
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
    $ch = curl_init('http://sms.verimor.com.tr/v2/send.json');
    curl_setopt_array($ch, array(
        CURLOPT_POST => TRUE,
        CURLOPT_RETURNTRANSFER => TRUE,
        CURLOPT_HTTPHEADER => array('Content-Type: application/json'),
        CURLOPT_POSTFIELDS => json_encode($sms_msg),
    ));
    $http_response = curl_exec($ch);
    $http_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
    if($http_code != 200){
      echo "$http_code $http_response\n";
      return false;
    }
    
    return $http_response;
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
```
**Tek Mesajın Çok Kişiye Gönderimi (HTTPS)**
```php
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
```

**SMS API ile çok mesajın çok kişiye gönderimi örneği**
----
Aşağıdaki örnek kodu kendinize özelleştirerek kullanabilirsiniz.
```php
<?php
 
function sendSMS($source_addr, $messages){
  $campaign = array(
    "username" => "xxxx",
    "password" => "xxxx",
    "source_addr" => $source_addr,
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
  "msg" => "Sayın Mehmet Öztürk. Doğum gününüzü kutlar, nice yıllarda beraber çalışmayı dileriz.",
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
```
**Çok Mesajın Çok Kişiye Gönderimi (HTTPS)**
```php
<?php
 
function sendSMS($source_addr, $messages){
  $campaign = array(
    "username" => "xxxx",
    "password" => "xxxx",
    "source_addr" => $source_addr,
//    "valid_for" => "48:00",
//    "send_at" => "2015-02-20 16:06:00",
//    "datacoding" => "0",
    "custom_id" => "1424441160.9331344",
    "messages" => $messages
  );
 
  $ch = curl_init('https://sms.verimor.com.tr/v2/send.json');
  curl_setopt_array($ch, array(
      CURLOPT_POST => TRUE,
      CURLOPT_RETURNTRANSFER => TRUE,
      CURLOPT_HTTPHEADER => array('Content-Type: application/json'),
      CURLOPT_POSTFIELDS => json_encode($campaign),
      CURLOPT_VERIFYPEER => 0, // sertifika dogrulamasi icin api dokumaninda belirtilen CRT dosyasini indirip bir klasore koyun ve asagi
daki ayarlari duzenleyip acin ve bu satiri kapatin
      // CURLOPT_CAINFO => "/tmp/rapidssl.crt",
      // CURLOPT_SSL_VERIFYHOST => 2,
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
 
  return $http_response;
}
 
$source_addr = "BASLIGIM";
$messages = array();
 
$messages[] = array(
  "msg" => "Sayın Mehmet Öztürk. Doğum gününüzü kutlar, nice yıllarda beraber çalışmayı dileriz.",
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
```
SMS API ‘den PUSH gönderim raporu alımı örneği (php)
----
Aşağıdaki örnek kodu kendinize özelleştirerek kullanabilirsiniz.
```php
<?php
 
$request = file_get_contents("php://input");
$json_request = json_decode($request);
 
// print_r($json_request);
 
foreach($json_request as $message_status){
	$campaign_id = $message_status->campaign_id; // 20210
	$campaign_custom_id = $message_status->campaign_custom_id; // 1424441160.9331343
	$message_id = $message_status->message_id; // 13582303
	$dest = $message_status->dest; // 905319876543
	$size = $message_status->size; // 1
	$sent_at = $message_status->sent_at;   // 2015-02-20 16:06:10
	$done_at = $message_status->done_at;   // 2015-02-20 16:06:10
	$status = $message_status->status;     // DELIVERED
	$gsm_error = $message_status->gsm_error; // 0
 
	echo "$message_id idli mesajin durumu: $status\n";
}
 
?>
```
**SMS API ‘den HTTP GET ile gönderim raporu alımı örneği (php)**
----
Aşağıdaki örnek kodu kendinize özelleştirerek kullanabilirsiniz.
```php
<?php
 
function queryBatch($campaign_id="", $campaign_custom_id="", $dest="", $greater_than="")
{
  $username="xxxx";
  $password=urlencode("xxxx");
  $url = "https://sms.verimor.com.tr/v2/status?username=$username&password=$password";
  
  if($campaign_id<>"")
    $url .= "&id=$campaign_id";
  else
    $url .= "&custom_id=$campaign_custom_id";
 
  if($dest<>"")
    $url .= "&dest=$dest";
  if($greater_than<>"")
    $url .= "&greater_than=$greater_than";
 
  $ch = curl_init($url);
  curl_setopt($ch, CURLOPT_RETURNTRANSFER, TRUE);
  $http_response = curl_exec($ch);
  $http_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
  if($http_code != 200){
    echo "$http_code $http_response\n";
    return false;
  }
  $json_response = json_decode($http_response);
  return $json_response;
}
 
function queryCampaign($campaign_id="", $campaign_custom_id="", $only_dest="")
{
  $last_message_id = "";
  while (true) {
    $messages = queryBatch($campaign_id, $campaign_custom_id, $only_dest, $last_message_id);
    if($messages===false){
      echo "Kampanya sorgulanamadı.\n";
      return false;
    }
 
    foreach ($messages as $msg) {
      $campaign_id = $msg->campaign_id; // 20210
      $campaign_custom_id = $msg->campaign_custom_id; // 1424441160.9331343
      $message_id = $msg->message_id; // 13582303
      $dest = $msg->dest; // 905319876543
      $size = $msg->size; // 1
      $sent_at = $msg->sent_at;   // 2015-02-20 16:06:10
      $done_at = $msg->done_at;   // 2015-02-20 16:06:10
      $status = $msg->status;     // DELIVERED
      $gsm_error = $msg->gsm_error; // 0
 
      echo "$dest telefonuna giden $message_id idli mesajin durumu: $status\n";
      $last_message_id = $message_id;
    }
 
    if(count($messages)<100)
      break;
  }
}
 
queryCampaign("20012"); // Kampanya ID'den sorgula (tüm kayıtlar)
queryCampaign("", "1424441160.9331343"); // Özel ID'den sorgula (tüm kayıtlar)
queryCampaign("20012","","905319876543"); // Verilen telefon numarasına giden mesajlar
queryCampaign("20012","","905319876543,905319876543"); // Verilen telefon numaralarına giden mesajlar
 
?>
```
**SMS API ile kalan krediyi sorgulama örneği (php)**
----
Aşağıdaki örnek kodu kendinize özelleştirerek kullanabilirsiniz.
```php
<?php
 
function queryCredits(){
  $username="xxxx";
  $password=urlencode("xxxx");
  $url= "http://sms.verimor.com.tr/v2/balance?username=$username&password=$password";
  $ch = curl_init($url);
  curl_setopt($ch, CURLOPT_RETURNTRANSFER, TRUE);
  $http_response = curl_exec($ch);
  $http_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
  if($http_code != 200){
    echo "$http_code $http_response\n";
    return false;
  }
 
  $balance = $http_response; // 4532
  echo "Mevcut krediniz: $balance";
  return $balance;
}
 
queryCredits();
```
**Kalan Krediyi Sorgulama (HTTPS)**
```php
<?php
 
function queryCredits(){
  $username="xxxx";
  $password=urlencode("xxxx");
  $url= "https://sms.verimor.com.tr/v2/balance?username=$username&password=$password";
  $ch = curl_init($url);
  curl_setopt($ch, CURLOPT_RETURNTRANSFER, TRUE);
  curl_setopt($ch, CURLOPT_CAINFO, "/sunucunuzda/bir/yol/rapidssl.crt"); // bu dosyayi api dokumani sayfamizdan indirin
  $http_response = curl_exec($ch);
  $http_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
  if($http_code != 200){
    echo "$http_code $http_response\n";
    return false;
  }
 
  $balance = $http_response; // 4532
  echo "Mevcut krediniz: $balance";
  return $balance;
}
 
queryCredits();
```
