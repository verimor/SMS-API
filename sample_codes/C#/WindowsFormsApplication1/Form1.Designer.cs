namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.kullaniciAdiTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sifreTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.telefonTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mesajTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.sendTB = new System.Windows.Forms.Button();
            this.baslikTB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // kullaniciAdiTB
            // 
            this.kullaniciAdiTB.Location = new System.Drawing.Point(82, 6);
            this.kullaniciAdiTB.Name = "kullaniciAdiTB";
            this.kullaniciAdiTB.Size = new System.Drawing.Size(190, 20);
            this.kullaniciAdiTB.TabIndex = 0;
            this.kullaniciAdiTB.Text = "90850xxxxxxx";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Kullanıcı Adı";
            // 
            // sifreTB
            // 
            this.sifreTB.Location = new System.Drawing.Point(82, 32);
            this.sifreTB.Name = "sifreTB";
            this.sifreTB.Size = new System.Drawing.Size(190, 20);
            this.sifreTB.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Şifre";
            // 
            // telefonTB
            // 
            this.telefonTB.Location = new System.Drawing.Point(82, 84);
            this.telefonTB.Name = "telefonTB";
            this.telefonTB.Size = new System.Drawing.Size(190, 20);
            this.telefonTB.TabIndex = 7;
            this.telefonTB.Text = "905321112233,905352223344";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Telefon";
            // 
            // mesajTB
            // 
            this.mesajTB.Location = new System.Drawing.Point(82, 110);
            this.mesajTB.Name = "mesajTB";
            this.mesajTB.Size = new System.Drawing.Size(190, 20);
            this.mesajTB.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Mesaj";
            // 
            // sendTB
            // 
            this.sendTB.Location = new System.Drawing.Point(82, 136);
            this.sendTB.Name = "sendTB";
            this.sendTB.Size = new System.Drawing.Size(190, 23);
            this.sendTB.TabIndex = 10;
            this.sendTB.Text = "Gönder";
            this.sendTB.UseVisualStyleBackColor = true;
            this.sendTB.Click += new System.EventHandler(this.sendTB_Click);
            // 
            // baslikTB
            // 
            this.baslikTB.Location = new System.Drawing.Point(82, 58);
            this.baslikTB.Name = "baslikTB";
            this.baslikTB.Size = new System.Drawing.Size(190, 20);
            this.baslikTB.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Başlık";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 165);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(260, 47);
            this.textBox1.TabIndex = 12;
            this.textBox1.Text = "NOT: Bu proje JSON.NET kütüphanesini kullanır. Şu adresten indirip proje referans" +
    "larına eklemelisiniz: http://www.newtonsoft.com/json";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 216);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.sendTB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mesajTB);
            this.Controls.Add(this.telefonTB);
            this.Controls.Add(this.baslikTB);
            this.Controls.Add(this.sifreTB);
            this.Controls.Add(this.kullaniciAdiTB);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox kullaniciAdiTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sifreTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox telefonTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox mesajTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button sendTB;
        private System.Windows.Forms.TextBox baslikTB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox1;
    }
}

