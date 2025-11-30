using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ChatClient
{
    public class SteganographyHelper
    {
        private static int j;
        public static Bitmap Gizle(Bitmap resim, string sifre)
        {
            // Orijinal resmi bozmamak için kopyasını alıyoruz
            Bitmap yeniResim = new Bitmap(resim);

            string gizlenecekYazi = sifre + "|";

            byte[] harfler = Encoding.UTF8.GetBytes(gizlenecekYazi);

            int harfSirasi = 0;
            int bitSirasi = 0;

            for (int i = 0; i < yeniResim.Width; i++)
            {
                for (int j = 0; j < yeniResim.Height; j++)
                {

                    if (harfSirasi >= harfler.Length)
                    {
                        return yeniResim;
                    }

                    Color piksel = yeniResim.GetPixel(i, j);

                    // Sıradaki harfin sıradaki bitini (0 mı 1 mi) buluyoruz
                    // Burası biraz matematiksel ama kural bu:
                    int suankiBit = (harfler[harfSirasi] >> bitSirasi) & 1;

                    // Pikselin Mavi (Blue) tonunun son bitini değiştiriyoruz
                    // 254 ile VE işlemi yapıp son biti sıfırlıyoruz, sonra kendi bitimizi ekliyoruz
                    int yeniMavi = (piksel.B & 254) | suankiBit;

                    Color yeniRenk = Color.FromArgb(piksel.R, piksel.G, yeniMavi);
                    yeniResim.SetPixel(i, j, yeniRenk);

                    bitSirasi++;

                    // Bir harf 8 bittir. 8 bit dolunca diğer harfe geçiyoruz
                    if (bitSirasi == 8)
                    {
                        bitSirasi = 0;
                        harfSirasi++;
                    }
                }
            }
            return yeniResim;
        }

        public static string Coz(Bitmap resim)
        {
            string cozulmusYazi = "";
            int bitSirasi = 0;
            int olusanHarfDegeri = 0;

            for (int i = 0; i < resim.Width; i++)
            {
                for (int j = 0; j < resim.Height; j++)
                {
                    Color piksel = resim.GetPixel(i, j);

                    // Mavi rengin son bitini alıyoruz (1 ile VE işlemi)
                    int sonBit = piksel.B & 1;

                    if (sonBit == 1)
                    {
                        olusanHarfDegeri += (1 << bitSirasi);
                    }

                    bitSirasi++;

                    if (bitSirasi == 8)
                    {
                        char harf = (char)olusanHarfDegeri;

                        if (harf == '|')
                        {
                            return cozulmusYazi;
                        }

                        cozulmusYazi += harf;

                        bitSirasi = 0;
                        olusanHarfDegeri = 0;
                    }
                }
            }
            return "";
        }
    }
}