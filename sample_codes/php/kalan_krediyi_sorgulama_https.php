<?php
// SMS API ile kalan krediyi sorgulama örneği (HTTPS)
// Aşağıdaki örnek kodu kendinize özelleştirerek kullanabilirsiniz.

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
