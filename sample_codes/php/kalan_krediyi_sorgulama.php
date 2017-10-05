<?php
// SMS API ile kalan krediyi sorgulama örneği (HTTP)
// Aşağıdaki örnek kodu kendinize özelleştirerek kullanabilirsiniz.

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
