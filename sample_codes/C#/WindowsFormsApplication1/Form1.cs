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
            // NOT: Bu kod JSON.NET kütüphanesini kullanır. Şu adresten indirip proje referanslarına eklemelisiniz: http://www.newtonsoft.com/json
            var sendMsg = new SmsIstegi();
            sendMsg.username = kullaniciAdiTB.Text;
            sendMsg.password = sifreTB.Text;
            sendMsg.source_addr = baslikTB.Text;
            sendMsg.messages = new Mesaj[] { new Mesaj(mesajTB.Text, telefonTB.Text) };

            string payload = JsonConvert.SerializeObject(sendMsg);
            
            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            string campaign_id = wc.UploadString("http://sms.verimor.com.tr/v2/send.json", payload);
            MessageBox.Show("Mesaj gönderildi, kampanya id: " + campaign_id);
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
