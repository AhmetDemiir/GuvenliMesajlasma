using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        TcpClient istemci;
        NetworkStream agAkisi;
        BinaryReader okuyucu;
        BinaryWriter yazici;
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void btnResimSec_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosyaSecici = new OpenFileDialog();
            dosyaSecici.Title = "Bir profil resmi seçiniz";
            dosyaSecici.Filter = "Resim Dosyaları|*.png;*.jpg;*.jpeg;*.bmp";

            if (dosyaSecici.ShowDialog() == DialogResult.OK)
            {
                pbProfilResmi.Image = new Bitmap(dosyaSecici.FileName);
            }
        }

        private void btnBaglan_Click(object sender, EventArgs e)
        {
            try
            {
                string kAdi = txtKullaniciAdi.Text;
                string sifre = txtSifre.Text;
                Bitmap profilResmi = (Bitmap)pbProfilResmi.Image;

                if (string.IsNullOrEmpty(kAdi) || profilResmi == null)
                {
                    MessageBox.Show("Eksik bilgi!");
                    return;
                }

                // Steganografi
                Bitmap sifreliResim = SteganographyHelper.Gizle(profilResmi, sifre);

                // Sunucuya Bağlan
                istemci = new TcpClient("127.0.0.1", 6060);
                agAkisi = istemci.GetStream();
                yazici = new BinaryWriter(agAkisi);
                okuyucu = new BinaryReader(agAkisi);

                // Verileri Gönder
                yazici.Write(kAdi);
                MemoryStream ms = new MemoryStream();
                sifreliResim.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] resimBaytlari = ms.ToArray();

                yazici.Write(resimBaytlari.Length);
                yazici.Write(resimBaytlari);

                // -------------------------------------------------------------------
                // --- BURASI DEĞİŞTİ: EKRANI HEMEN DEĞİŞTİRME, ÖNCE CEVAP BEKLE ---
                // -------------------------------------------------------------------

                // Sunucudan "LOGIN_OK" mesajı gelmesini bekliyoruz.
                // Eğer şifre yanlışsa sunucu bağlantıyı kestiği için burası hata verir ve catch'e düşer.
                string sunucuCevabi = okuyucu.ReadString();

                if (sunucuCevabi == "LOGIN_OK")
                {
                    // ONAY GELDİ! Şimdi ekranı değiştirebiliriz.
                    groupBox2.Visible = false;
                    grpSohbet.Visible = true;

                    this.Text = "Chat İstemcisi - Ben: " + kAdi;
                    grpSohbet.Location = groupBox2.Location;

                    // DİNLEME MODUNU BAŞLAT
                    Thread dinleThread = new Thread(SunucudanMesajDinle);
                    dinleThread.Start();
                }
                else
                {
                    // Sunucu "LOGIN_OK" dışında bir şey dediyse (ihtimal düşük ama olsun)
                    MessageBox.Show("Sunucu girişi onaylamadı.");
                }
                // -------------------------------------------------------------------
            }
            catch (Exception ex)
            {
                // Eğer şifre yanlışsa sunucu bağlantıyı "küt" diye kestiği için
                // okuyucu.ReadString() satırı hata verecek ve buraya düşecektir.
                MessageBox.Show("Giriş Başarısız!\nSunucu bağlantıyı reddetti.\n(İsim kullanımda olabilir veya şifre yanlıştır.)");
            }
        }

        private void SunucudanMesajDinle()
        {
            while (istemci.Connected)
            {
                try
                {
                    string gelenPaket = okuyucu.ReadString();
                    string[] parcalar = gelenPaket.Split('|');

                    string gonderen = parcalar[0];
                    string icerik = parcalar[1];

                    // EĞER MESAJ SUNUCUDAN GELEN LİSTE İSE
                    if (gonderen == "SERVER")
                    {
                        // icerik şöyledir: "Ahmet (Online);Ayse (Offline);"
                        string[] kullanicilar = icerik.Split(';');

                        // ListBox'ı güncelle (Invoke ile arayüze erişiyoruz)
                        lstKullanicilar.Invoke((MethodInvoker)delegate {
                            lstKullanicilar.Items.Clear(); // Eskileri sil
                            foreach (string k in kullanicilar)
                            {
                                if (!string.IsNullOrEmpty(k))
                                    lstKullanicilar.Items.Add(k);
                            }
                        });
                    }
                    // EĞER NORMAL BİR KULLANICIDAN GELEN MESAJ İSE
                    else
                    {
                        string cozulmusMesaj = DesHelper.Coz(icerik, txtSifre.Text);
                        rtbMesajlar.Invoke((MethodInvoker)delegate {
                            rtbMesajlar.AppendText(gonderen + ": " + cozulmusMesaj + "\n");
                        });
                    }
                }
                catch { break; }
            }
        }
        private void grpSohbet_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void btnGonder_Click(object sender, EventArgs e)
        {
            // 1. Kime göndereceğiz? (Yeni kutudan alıyoruz)
            string alici = txtAlici.Text.Trim(); // Trim boşlukları siler
            string mesaj = txtMesaj.Text.Trim();

            if (string.IsNullOrEmpty(alici) || string.IsNullOrEmpty(mesaj))
            {
                MessageBox.Show("Lütfen alıcı ismi ve mesaj giriniz.");
                return;
            }

            // 2. Mesajı Formatla ve Şifrele
            // Kendi şifremizle mesajı şifreliyoruz
            string sifreliMesaj = DesHelper.Sifrele(mesaj, txtSifre.Text);

            // 3. Sunucuya Gönder: "ALICI|SIFRELI_MESAJ"
            // Artık elle : koymana gerek yok, kod koyuyor.
            yazici.Write(alici + "|" + sifreliMesaj);

            // 4. Ekrana Yazdır
            rtbMesajlar.AppendText("Ben -> " + alici + ": " + mesaj + "\n");

            // Mesaj kutusunu temizle ama alıcı ismi kalsın (sohbet sürsün diye)
            txtMesaj.Clear();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (istemci != null && istemci.Connected)
            {
                istemci.Close(); // Bağlantıyı temiz bir şekilde kes
            }
            Application.Exit(); // Uygulamayı tamamen kapat
        }

        private void lstKullanicilar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstKullanicilar.SelectedItem == null) return;

            // Seçilen satır: "Ahmet (Online)"
            string secilen = lstKullanicilar.SelectedItem.ToString();

            // Sadece ismi al (Boşluğa kadar olan kısmı)
            string isim = secilen.Split(' ')[0];

            // Kime kutusuna yaz
            txtAlici.Text = isim;
        }
    }
}
