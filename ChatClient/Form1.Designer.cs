namespace ChatClient
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBaglan = new System.Windows.Forms.Button();
            this.pbProfilResmi = new System.Windows.Forms.PictureBox();
            this.btnResimSec = new System.Windows.Forms.Button();
            this.txtSifre = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtKullaniciAdi = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grpSohbet = new System.Windows.Forms.GroupBox();
            this.btnGonder = new System.Windows.Forms.Button();
            this.txtMesaj = new System.Windows.Forms.TextBox();
            this.rtbMesajlar = new System.Windows.Forms.RichTextBox();
            this.lstKullanicilar = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAlici = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbProfilResmi)).BeginInit();
            this.grpSohbet.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnBaglan);
            this.groupBox2.Controls.Add(this.pbProfilResmi);
            this.groupBox2.Controls.Add(this.btnResimSec);
            this.groupBox2.Controls.Add(this.txtSifre);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtKullaniciAdi);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(859, 710);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Giriş Yap";
            // 
            // btnBaglan
            // 
            this.btnBaglan.Location = new System.Drawing.Point(435, 580);
            this.btnBaglan.Name = "btnBaglan";
            this.btnBaglan.Size = new System.Drawing.Size(199, 77);
            this.btnBaglan.TabIndex = 6;
            this.btnBaglan.Text = "Sunucuya Bağlan:";
            this.btnBaglan.UseVisualStyleBackColor = true;
            this.btnBaglan.Click += new System.EventHandler(this.btnBaglan_Click);
            // 
            // pbProfilResmi
            // 
            this.pbProfilResmi.Location = new System.Drawing.Point(74, 216);
            this.pbProfilResmi.Name = "pbProfilResmi";
            this.pbProfilResmi.Size = new System.Drawing.Size(368, 331);
            this.pbProfilResmi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbProfilResmi.TabIndex = 5;
            this.pbProfilResmi.TabStop = false;
            // 
            // btnResimSec
            // 
            this.btnResimSec.Location = new System.Drawing.Point(74, 161);
            this.btnResimSec.Name = "btnResimSec";
            this.btnResimSec.Size = new System.Drawing.Size(97, 23);
            this.btnResimSec.TabIndex = 4;
            this.btnResimSec.Text = "Resim Seç:";
            this.btnResimSec.UseVisualStyleBackColor = true;
            this.btnResimSec.Click += new System.EventHandler(this.btnResimSec_Click);
            // 
            // txtSifre
            // 
            this.txtSifre.Location = new System.Drawing.Point(165, 91);
            this.txtSifre.Name = "txtSifre";
            this.txtSifre.Size = new System.Drawing.Size(125, 22);
            this.txtSifre.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(71, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Şifre:";
            // 
            // txtKullaniciAdi
            // 
            this.txtKullaniciAdi.Location = new System.Drawing.Point(165, 41);
            this.txtKullaniciAdi.Name = "txtKullaniciAdi";
            this.txtKullaniciAdi.Size = new System.Drawing.Size(125, 22);
            this.txtKullaniciAdi.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(71, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Kullanıcı Adı:";
            // 
            // grpSohbet
            // 
            this.grpSohbet.Controls.Add(this.txtAlici);
            this.grpSohbet.Controls.Add(this.label1);
            this.grpSohbet.Controls.Add(this.btnGonder);
            this.grpSohbet.Controls.Add(this.txtMesaj);
            this.grpSohbet.Controls.Add(this.rtbMesajlar);
            this.grpSohbet.Controls.Add(this.lstKullanicilar);
            this.grpSohbet.Location = new System.Drawing.Point(905, 33);
            this.grpSohbet.Name = "grpSohbet";
            this.grpSohbet.Size = new System.Drawing.Size(826, 689);
            this.grpSohbet.TabIndex = 4;
            this.grpSohbet.TabStop = false;
            this.grpSohbet.Text = "Sohbet Ekranı";
            this.grpSohbet.Visible = false;
            // 
            // btnGonder
            // 
            this.btnGonder.Location = new System.Drawing.Point(33, 367);
            this.btnGonder.Name = "btnGonder";
            this.btnGonder.Size = new System.Drawing.Size(159, 37);
            this.btnGonder.TabIndex = 9;
            this.btnGonder.Text = "Gönder";
            this.btnGonder.UseVisualStyleBackColor = true;
            this.btnGonder.Click += new System.EventHandler(this.btnGonder_Click);
            // 
            // txtMesaj
            // 
            this.txtMesaj.Location = new System.Drawing.Point(6, 327);
            this.txtMesaj.Name = "txtMesaj";
            this.txtMesaj.Size = new System.Drawing.Size(226, 22);
            this.txtMesaj.TabIndex = 8;
            // 
            // rtbMesajlar
            // 
            this.rtbMesajlar.Location = new System.Drawing.Point(261, 23);
            this.rtbMesajlar.Name = "rtbMesajlar";
            this.rtbMesajlar.Size = new System.Drawing.Size(470, 326);
            this.rtbMesajlar.TabIndex = 7;
            this.rtbMesajlar.Text = "";
            // 
            // lstKullanicilar
            // 
            this.lstKullanicilar.FormattingEnabled = true;
            this.lstKullanicilar.ItemHeight = 16;
            this.lstKullanicilar.Location = new System.Drawing.Point(6, 21);
            this.lstKullanicilar.Name = "lstKullanicilar";
            this.lstKullanicilar.Size = new System.Drawing.Size(204, 244);
            this.lstKullanicilar.TabIndex = 0;
            this.lstKullanicilar.SelectedIndexChanged += new System.EventHandler(this.lstKullanicilar_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 299);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Kime:";
            // 
            // txtAlici
            // 
            this.txtAlici.Location = new System.Drawing.Point(79, 296);
            this.txtAlici.Name = "txtAlici";
            this.txtAlici.Size = new System.Drawing.Size(100, 22);
            this.txtAlici.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1777, 826);
            this.Controls.Add(this.grpSohbet);
            this.Controls.Add(this.groupBox2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbProfilResmi)).EndInit();
            this.grpSohbet.ResumeLayout(false);
            this.grpSohbet.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnBaglan;
        private System.Windows.Forms.PictureBox pbProfilResmi;
        private System.Windows.Forms.Button btnResimSec;
        private System.Windows.Forms.TextBox txtSifre;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtKullaniciAdi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox grpSohbet;
        private System.Windows.Forms.Button btnGonder;
        private System.Windows.Forms.TextBox txtMesaj;
        private System.Windows.Forms.RichTextBox rtbMesajlar;
        private System.Windows.Forms.ListBox lstKullanicilar;
        private System.Windows.Forms.TextBox txtAlici;
        private System.Windows.Forms.Label label1;
    }
}