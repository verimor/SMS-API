using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void sendTB_Click(object sender, EventArgs e)
        {
            // Bu kod JSON.NET kütüphanesini kullanır. Şu adresten indirip proje referanslarına eklemelisiniz: http://www.newtonsoft.com/json
            // Sunucu IP adresinizi https://oim.verimor.com.tr/sms_settings/edit sayfasından girmiş olmanız gerekir.
            // Girmezseniz 401 hatası alırsınız.
            var smsIstegi = new SmsIstegi();
            smsIstegi.username = kullaniciAdiTB.Text;
            smsIstegi.password = sifreTB.Text;
            smsIstegi.source_addr = baslikTB.Text;
            smsIstegi.messages = new Mesaj[] { new Mesaj(mesajTB.Text, telefonTB.Text) };
            IstegiGonder(smsIstegi);
        }

        private void IstegiGonder(SmsIstegi istek)
        {
            string payload = JsonConvert.SerializeObject(istek);

            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            wc.Headers["Content-Type"] = "application/json";

            try
            {
                string campaign_id = wc.UploadString("http://sms.verimor.com.tr/v2/send.json", payload);
                MessageBox.Show("Mesaj gönderildi, kampanya id: " + campaign_id);
            }
            catch (WebException ex) // 400 hatalarında response body'de hatanın ne olduğunu yakalıyoruz
            {
                if (ex.Status == WebExceptionStatus.ProtocolError) // 400 hataları
                {
                    var responseBody = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    MessageBox.Show("Mesaj gönderilemedi, dönen hata: " + responseBody);
                }
                else // diğer hatalar
                {
                    // MessageBox.Show("Mesaj gönderilemedi, dönen hata: " + ex.Status);
                    throw;
                }
            }
        }
    }

    class Mesaj
    {
        public string msg { get; set; }
        public string dest { get; set; }

        public Mesaj() { }

        public Mesaj(string msg, string dest)
        {
            this.msg = msg;
            this.dest = dest;
        }
    }
    class SmsIstegi
    {
        public string username { get; set; }
        public string password { get; set; }
        public string source_addr { get; set; }
        public Mesaj[] messages { get; set; }
    }
}
