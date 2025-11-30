using System;
using System.Drawing; // Resim için
using System.IO;      // Veri okuma/yazma için
using System.Net;     // Ağ işlemleri (Socket)
using System.Net.Sockets;
using System.Threading; // Arka planda çalıştırmak için
using System.Windows.Forms;

namespace ChatServer
{
    public partial class Form1 : Form
    {
        // Sunucu dinleyicisi
        TcpListener serverSocket;
        Thread dinlemeThread;

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; // Basit hata engellemek için
        }

        // Form açılınca sunucu otomatik başlasın
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // 6060 portundan yayın yapmaya başla
                serverSocket = new TcpListener(IPAddress.Any, 6060);
                serverSocket.Start();

                txtLog.AppendText("Sunucu başlatıldı... Bağlantılar bekleniyor.\n");

                // Arka planda sürekli dinlemesi için Thread başlatıyoruz
                dinlemeThread = new Thread(ClientDinle);
                dinlemeThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sunucu hatası: " + ex.Message);
            }
        }

        // Sürekli yeni müşteri var mı diye bakan fonksiyon
        private void ClientDinle()
        {
            while (true)
            {
                try
                {
                    // Birisi bağlandığı anda onu yakala
                    TcpClient gelenClient = serverSocket.AcceptTcpClient();
                    txtLog.AppendText(">> Yeni bir istemci bağlandı!\n");

                    // Gelen veriyi okumak için başka bir fonksiyona yolla
                    Thread clientThread = new Thread(() => VeriOku(gelenClient));
                    clientThread.Start();
                }
                catch { break; }
            }
        }

        private void VeriOku(TcpClient client)
        {
            NetworkStream agAkisi = client.GetStream();
            BinaryReader okuyucu = new BinaryReader(agAkisi);

            try
            {
                // 1. Önce Kullanıcı Adını Oku
                string gelenKullaniciAdi = okuyucu.ReadString();

                // 2. Resim Boyutunu Oku
                int resimBoyutu = okuyucu.ReadInt32();

                // 3. Resim Verisini (Byte Array) Oku
                byte[] resimBaytlari = okuyucu.ReadBytes(resimBoyutu);

                // Baytları tekrar Resim dosyasına çevir
                using (MemoryStream ms = new MemoryStream(resimBaytlari))
                {
                    Bitmap gelenResim = new Bitmap(ms);

                    // 4. Steganografi ile Şifreyi Çöz!
                    string cozulmusSifre = SteganographyHelper.Coz(gelenResim);

                    // Ekrana yazdıralım
                    txtLog.AppendText("Kullanıcı: " + gelenKullaniciAdi + "\n");
                    txtLog.AppendText("Gizli Şifre (Resimden Çıkan): " + cozulmusSifre + "\n");
                    txtLog.AppendText("----------------------------------\n");
                }
            }
            catch (Exception ex)
            {
                txtLog.AppendText("Hata: " + ex.Message + "\n");
            }
        }
    }
}