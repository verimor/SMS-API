<?php
// SMS API ‘den HTTP GET ile gönderim raporu alımı örneği
// Aşağıdaki örnek kodu kendinize özelleştirerek kullanabilirsiniz.

function queryBatch($campaign_id="", $campaign_custom_id="", $dest="", $greater_than="")
{
  $username="xxxx"; // https://oim.verimor.com.tr/sms_settings/edit adresinden öğrenebilirsiniz.
  $password=urlencode("xxxx"); // https://oim.verimor.com.tr/sms_settings/edit adresinden belirlemeniz gerekir.
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
