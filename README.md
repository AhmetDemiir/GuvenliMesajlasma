### 2. GuvenliMesajlasma (C#)
Client-Server (İstemci-Sunucu) mimarisini ve şifreleme mantığını vurgulayan bir yapı hazırladım.

```markdown
# 🔐 Güvenli Mesajlaşma Uygulaması

<p align="left">
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white" alt="C#" />
  <img src="https://img.shields.io/badge/.NET_Core-512BD4?style=for-the-badge&logo=.net&logoColor=white" alt=".NET" />
  <img src="https://img.shields.io/badge/Cryptography-000000?style=for-the-badge&logo=security&logoColor=white" alt="Crypto" />
</p>

## 📋 Proje Özeti
C# ile geliştirilmiş, ağ üzerinde iletişim kuran istemciler (clients) arasında kriptografik şifreleme yöntemleri kullanarak iletişimi gizli ve güvenli hale getiren bir mesajlaşma uygulamasıdır. 

## 🏗️ Mimari Yapı
Proje **İstemci-Sunucu (Client-Server)** mimarisi üzerine inşa edilmiştir ve iki ana modülden oluşur:
- 🖥️ **ChatServer:** İstemcilerin bağlandığı, mesaj trafiğini yöneten ve güvenlik doğrulamalarını yapan merkez sunucu.
- 📱 **ChatClient:** Kullanıcıların giriş yaptığı, mesaj gönderip aldığı uç nokta uygulaması.

## ✨ Özellikler
- ✅ **Uçtan Uca Şifreleme:** Kriptografik algoritmalar ile mesaj içeriklerinin korunması.
- ✅ **Soket Programlama:** TCP/IP protokolü üzerinden kesintisiz ve hızlı veri aktarımı.
- ✅ **Güvenli Giriş (Login):** Kullanıcı adı ve parola ile login onayı ve güvenlik kontrolleri.
- ✅ **Çoklu İstemci Desteği:** Aynı anda birden fazla kullanıcının sunucuya bağlanabilmesi.

## 🚀 Başlarken
Projeyi yerel ortamınızda çalıştırmak için:
1. Repoyu klonlayın:
   ```bash
   git clone [https://github.com/AhmetDemiir/GuvenliMesajlasma.git](https://github.com/AhmetDemiir/GuvenliMesajlasma.git)
2. Visual Studio üzerinden GuvenliMesajlasma.sln çözüm (solution) dosyasını açın.

3. Önce ChatServer projesini başlatarak sunucuyu ayağa kaldırın.

4. Ardından birden fazla ChatClient örneği (instance) çalıştırarak şifreli mesajlaşmayı test edin.
