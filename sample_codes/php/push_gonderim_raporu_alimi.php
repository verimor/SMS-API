<?php
// SMS API ‘den PUSH gönderim raporu alımı örneği
// Bu dosyayı sunucunuza yerleştirip https://oim.verimor.com.tr/sms_settings/edit adresinden PUSH URL olarak ayarlayıp kullanabilirsiniz. 
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
