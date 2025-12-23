using System;
using System.Text;
using System.Security.Cryptography; // Şifreleme kütüphanesi
using System.IO;

namespace ChatClient
{
    public class DesHelper
    {
        // DES algoritması anahtarın tam 8 karakter (64 bit) olmasını ister.
        // Eğer şifre "12345" ise (5 karakter), sonuna boşluk ekleyip 8'e tamamlayan fonksiyon.
        private static byte[] AnahtariDuzenle(string anahtar)
        {
            byte[] anahtarBaytlari = Encoding.UTF8.GetBytes(anahtar);
            byte[] tamAnahtar = new byte[8]; // DES için 8 bayt şart

            for (int i = 0; i < 8; i++)
            {
                if (i < anahtarBaytlari.Length)
                    tamAnahtar[i] = anahtarBaytlari[i];
                else
                    tamAnahtar[i] = 32; // Boşluk (Space) karakteri ile doldur
            }
            return tamAnahtar;
        }

        // ŞİFRELEME (Encrypt)
        public static string Sifrele(string acikMetin, string anahtar)
        {
            try
            {
                byte[] anahtarBaytlari = AnahtariDuzenle(anahtar);
                byte[] iv = anahtarBaytlari;

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();

                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(anahtarBaytlari, iv), CryptoStreamMode.Write);

                StreamWriter sw = new StreamWriter(cs);
                sw.Write(acikMetin);
                sw.Flush();
                cs.FlushFinalBlock();
                sw.Flush();

                // Şifrelenmiş veriyi okunabilir String'e (Base64) çevir
                return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
            }
            catch
            {
                return "Hata";
            }
        }

        // ÇÖZME (Decrypt)
        public static string Coz(string sifreliMetin, string anahtar)
        {
            try
            {
                byte[] anahtarBaytlari = AnahtariDuzenle(anahtar);
                byte[] iv = anahtarBaytlari;

                byte[] buffer = Convert.FromBase64String(sifreliMetin);

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream(buffer);

                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(anahtarBaytlari, iv), CryptoStreamMode.Read);

                StreamReader sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            catch
            {
                return "Çözülemedi";
            }
        }
    }
}