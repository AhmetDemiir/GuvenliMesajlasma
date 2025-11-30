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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
                // 1. Resmi ve Bilgileri Al
                string kAdi = txtKullaniciAdi.Text;
                string sifre = txtSifre.Text;
                Bitmap profilResmi = (Bitmap)pbProfilResmi.Image;

                if (string.IsNullOrEmpty(kAdi) || profilResmi == null)
                {
                    MessageBox.Show("Lütfen kullanıcı adı ve resim seçiniz.");
                    return;
                }

                // 2. STEGANOGRAFİ: Şifreyi resmin içine gizle
                Bitmap sifreliResim = SteganographyHelper.Gizle(profilResmi, sifre);

                // 3. Sunucuya Bağlan (Localhost, 6060 portu)
                TcpClient istemci = new TcpClient("127.0.0.1", 6060);
                NetworkStream agAkisi = istemci.GetStream();
                BinaryWriter yazici = new BinaryWriter(agAkisi);

                // 4. Verileri Gönder

                // A) Kullanıcı Adını Gönder
                yazici.Write(kAdi);

                // B) Resmi Bayt Dizisine (Byte Array) Çevir
                MemoryStream ms = new MemoryStream();
                // ÖNEMLİ: PNG formatında kaydetmeliyiz, JPG veri kaybı yapar şifre bozulur!
                sifreliResim.Save(ms, ImageFormat.Png);
                byte[] resimBaytlari = ms.ToArray();

                // C) Resim Boyutunu ve Kendisini Gönder
                yazici.Write(resimBaytlari.Length); // Boyut
                yazici.Write(resimBaytlari);        // Resim

                MessageBox.Show("Başarıyla kayıt olundu ve sunucuya gönderildi!");

                // Formu kapatma veya Chat ekranına geçiş kodları buraya gelecek...
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bağlantı hatası: " + ex.Message);
            }
        }
    }
}
