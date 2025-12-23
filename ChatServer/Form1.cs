using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace ChatServer
{
    public partial class Form1 : Form
    {
        TcpListener serverSocket;
        Thread dinlemeThread;

        // 1. Online Kullanıcılar (Anlık bağlı olanlar)
        public static Dictionary<string, TcpClient> OnlineClients = new Dictionary<string, TcpClient>();

        // 2. Kullanıcı Şifreleri
        public static Dictionary<string, string> ClientKeys = new Dictionary<string, string>();

        // 3. OFFLINE MESAJ KUTUSU (Kime -> Mesaj Listesi)
        public static Dictionary<string, List<string>> OfflineMessages = new Dictionary<string, List<string>>();

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                serverSocket = new TcpListener(IPAddress.Any, 6060);
                serverSocket.Start();
                txtLog.AppendText("Sunucu başlatıldı... 6060 portu dinleniyor.\n");

                dinlemeThread = new Thread(ClientDinle);
                dinlemeThread.Start();
            }
            catch (Exception ex) { MessageBox.Show("Server Hatası: " + ex.Message); }
        }
        private void ClientDinle()
        {
            while (true)
            {
                try
                {
                    TcpClient gelenClient = serverSocket.AcceptTcpClient();
                    Thread clientThread = new Thread(() => IstemciYonet(gelenClient));
                    clientThread.Start();
                }
                catch { break; }
            }
        }
        private void IstemciYonet(TcpClient client)
        {
            string bagliKullaniciAdi = "";
            NetworkStream agAkisi = client.GetStream();
            BinaryReader okuyucu = new BinaryReader(agAkisi);
            BinaryWriter yazici = new BinaryWriter(agAkisi);

            try
            {
                // --- BÖLÜM 1: LOGIN İŞLEMİ ---
                bagliKullaniciAdi = okuyucu.ReadString();
                int resimBoyutu = okuyucu.ReadInt32();
                byte[] resimBaytlari = okuyucu.ReadBytes(resimBoyutu);

                using (MemoryStream ms = new MemoryStream(resimBaytlari))
                {
                    Bitmap gelenResim = new Bitmap(ms);
                    string cozulmusSifre = SteganographyHelper.Coz(gelenResim);

                    if (OnlineClients.ContainsKey(bagliKullaniciAdi))
                    {
                        string gercekSifre = ClientKeys[bagliKullaniciAdi];

                        if (gercekSifre != cozulmusSifre)
                        {
                            txtLog.AppendText($">> [GÜVENLİK] {bagliKullaniciAdi} adına yanlış şifreyle giriş denemesi engellendi.\n");

                            client.Close();
                            return;
                        }
                        else
                        {
                            txtLog.AppendText($">> {bagliKullaniciAdi} tekrar bağlandı (Oturum Tazeleme).\n");
                            OnlineClients.Remove(bagliKullaniciAdi);
                        }
                    }

                    if (OnlineClients.ContainsKey(bagliKullaniciAdi))
                        OnlineClients.Remove(bagliKullaniciAdi);

                    if (ClientKeys.ContainsKey(bagliKullaniciAdi))
                        ClientKeys[bagliKullaniciAdi] = cozulmusSifre;
                    else
                        ClientKeys.Add(bagliKullaniciAdi, cozulmusSifre);

                    OnlineClients.Add(bagliKullaniciAdi, client);

                    yazici.Write("LOGIN_OK");

                    txtLog.AppendText($">> {bagliKullaniciAdi} Online oldu. Şifre: {cozulmusSifre}\n");

                    KullaniciListesiniYayinla();

                    // --- EĞER OFFLINE MESAJ VARSA GÖNDER ---
                    if (OfflineMessages.ContainsKey(bagliKullaniciAdi))
                    {
                        List<string> bekleyenMesajlar = OfflineMessages[bagliKullaniciAdi];
                        foreach (string msg in bekleyenMesajlar)
                        {
                            yazici.Write(msg);
                            txtLog.AppendText($"[OFFLINE İLETİLDİ] -> {bagliKullaniciAdi}\n");
                        }
                        OfflineMessages.Remove(bagliKullaniciAdi);
                    }
                }

                // --- BÖLÜM 2: MESAJLAŞMA ---
                while (client.Connected)
                {
                    string gelenPaket = okuyucu.ReadString();
                    string[] parcalar = gelenPaket.Split('|');
                    if (parcalar.Length < 2) continue;

                    string aliciIsmi = parcalar[0];
                    string sifreliMesaj = parcalar[1];

                    // 1. Gönderenin şifresiyle ÇÖZ
                    string gonderenSifre = ClientKeys[bagliKullaniciAdi];
                    string acikMesaj = DesHelper.Coz(sifreliMesaj, gonderenSifre);

                    txtLog.AppendText($"[Mesaj] {bagliKullaniciAdi} -> {aliciIsmi}: {acikMesaj}\n");

                    // 2. Alıcının şifresi VAR MI? (Daha önce hiç girmemişse şifreleyemeyiz)
                    if (ClientKeys.ContainsKey(aliciIsmi))
                    {
                        string aliciSifre = ClientKeys[aliciIsmi];
                        string tekrarSifreliMesaj = DesHelper.Sifrele(acikMesaj, aliciSifre);
                        string paket = $"{bagliKullaniciAdi}|{tekrarSifreliMesaj}";

                        // ALICI ONLINE MI?
                        if (OnlineClients.ContainsKey(aliciIsmi))
                        {
                            try
                            {
                                TcpClient aliciSocket = OnlineClients[aliciIsmi];
                                NetworkStream aliciAkis = aliciSocket.GetStream();
                                BinaryWriter aliciYazici = new BinaryWriter(aliciAkis);
                                aliciYazici.Write(paket);
                            }
                            catch
                            {
                                OfflineaEkle(aliciIsmi, paket);
                            }
                        }
                        else
                        {
                            OfflineaEkle(aliciIsmi, paket);
                            txtLog.AppendText($"   -> {aliciIsmi} çevrimdışı. Mesaj saklandı.\n");
                        }
                    }
                    else
                    {
                        txtLog.AppendText($"   -> HATA: {aliciIsmi} sistemde kayıtlı değil (Şifresi yok).\n");
                    }
                }
            }
            catch
            {
                if (bagliKullaniciAdi != "")
                {
                    OnlineClients.Remove(bagliKullaniciAdi);
                    txtLog.AppendText($">> {bagliKullaniciAdi} bağlantısı koptu (Offline).\n");
                    KullaniciListesiniYayinla();
                }
            }
        }
        private void OfflineaEkle(string alici, string paket)
        {
            if (!OfflineMessages.ContainsKey(alici))
            {
                OfflineMessages.Add(alici, new List<string>());
            }
            OfflineMessages[alici].Add(paket);
        }
        private void KullaniciListesiniYayinla()
        {
            string listeVerisi = "";

            foreach (var kullanici in ClientKeys.Keys)
            {
                string durum = OnlineClients.ContainsKey(kullanici) ? "(Online)" : "(Offline)";
                listeVerisi += kullanici + " " + durum + ";";
            }

            foreach (var client in OnlineClients.Values)
            {
                try
                {
                    NetworkStream ns = client.GetStream();
                    BinaryWriter bw = new BinaryWriter(ns);
                    bw.Write("SERVER|" + listeVerisi);
                }
                catch { }
            }
        }
    }
}