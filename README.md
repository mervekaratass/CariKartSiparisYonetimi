# Cari Kart ve Sipariş Yönetimi 📝
<p> 📌 Bu proje, bir teknik değerlendirme ödevi kapsamında geliştirdiğim basit bir Cari Kart ve Sipariş Yönetimi uygulamasıdır. </p>
<p> 📌 Amaç gelişmiş bir ticari uygulama değil; küçük, çalışan ve anlaşılır bir çözüm ortaya koymaktır. </p>

#### GEREKSİNİMLER 🛠
- ✅ Web projesi:
  ![ASP.NET Core MVC](https://img.shields.io/badge/asp.net%20core%20mvc-%231BA3E8.svg?style=for-the-badge&logo=dotnet&logoColor=white)
- ✅ Veri tabanı:
  ![MsSQL Server](https://img.shields.io/badge/mssql%20server-%23CC2927.svg?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)
- ✅ Arayüz:
  ![Bootstrap](https://img.shields.io/badge/bootstrap-%238511FA.svg?style=for-the-badge&logo=bootstrap&logoColor=white)

#### PROJEDE KULLANILAN TEKNOLOJİLER VE KÜTÜPHANELER 🛠️
<p>
  <img alt="C#" src="https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white" />
  <img alt=".NET" src="https://img.shields.io/badge/.NET%208-5C2D91?style=for-the-badge&logo=.net&logoColor=white" />
  <img alt="Entity Framework" src="https://img.shields.io/badge/entity%20framework%20core-%2358B9C9.svg?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img alt="JavaScript" src="https://img.shields.io/badge/javascript-%23323330.svg?style=for-the-badge&logo=javascript&logoColor=%23F7DF1E" />
  <img alt="Visual Studio" src="https://img.shields.io/badge/Visual%20Studio-5C2D91.svg?style=for-the-badge&logo=visualstudio&logoColor=white" />
  <img alt="Github" src="https://img.shields.io/badge/github-%23121011.svg?style=for-the-badge&logo=github&logoColor=white" />
</p>

#### 📫 NASIL BİR PROJE OLUŞTURDUK?
<p>Bu proje, firmaların cari kartlarını tanımlayıp bu carilere ait siparişleri oluşturabildiği ve takip edebildiği bir yönetim uygulamasıdır.</p>

➡️ 1- Cari Kart

- ✅ Yeni cari kart oluşturulabilir.
- ✅ Mevcut cari kartlar listelenebilir.
- ✅ Cari kart bilgileri düzenlenebilir.
- ✅ Cari kart pasife alınabilir veya tekrar aktif edilebilir.
- ✅ Cari karta JPG veya PNG formatında logo yüklenebilir.

➡️ 2- Sipariş

- ✅ Tanımlı ve aktif bir cari için sipariş oluşturulabilir.
- ✅ Bir siparişe birden fazla ürün/hizmet satırı eklenebilir.
- ✅ Satır tutarı (Miktar × Birim Fiyat) ve sipariş toplamı ekranda anlık hesaplanır.
- ✅ Oluşturulan siparişler Sipariş No, Tarih, Cari Adı ve Toplam Tutar bilgileriyle listelenir.
- ✅ Bir sipariş seçildiğinde detayları görüntülenebilir.

## PROJE DETAYLARI 📝

Proje, .NET 8 ve ASP.NET Core MVC ile geliştirilmiştir. Veri tabanı olarak MsSQL (SQL Server Express) kullanılmış, veri tabanı işlemleri için **Entity Framework Core** ve **Code First** yaklaşımı benimsenmiştir.

Ödevin beklentisi doğrultusunda proje tek katmanda, klasörlerle ayrılmış sade bir yapıda tutulmuştur. Doğrulama işlemleri için ek bir kütüphane kullanılmamış, **Data Annotations** tercih edilmiştir.

📌 Projede cari kartlar veri tabanından silinmez; **pasife alınır**. Böylece cariye ait geçmiş siparişler bozulmaz ve işlem istenildiğinde geri alınabilir. Pasif bir cariye yeni sipariş açılamaz.

🎯 Veri tabanı bağlantı yolu `appsettings.json` içinde tutulmaktadır. Farklı bir SQL Server kullanmak için yalnızca `Server=` kısmını değiştirmek yeterlidir.

```json
"ConnectionStrings": {
  "VarsayilanBaglanti": "Server=.\\SQLEXPRESS;Database=CariKartSiparisDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

### 🚀 Proje Nasıl Çalıştırılır?

- ⚡ Bilgisayarınızda **.NET 8 SDK** ve **SQL Server Express** kurulu olmalıdır.
- ⚡ `CariKartSiparisYonetimi.sln` dosyasını Visual Studio ile açıp F5'e basmanız yeterlidir.
- ⚡ Komut satırından çalıştırmak isterseniz:

```bash
dotnet run --project CariKartSiparisYonetimi
```

### 🗄️ Veri Tabanı Nasıl Oluşturulur?

📌 Veri tabanı **uygulama ilk çalıştığında otomatik olarak oluşturulur**. `Program.cs` içindeki `Database.Migrate()` çağrısı, `Migrations` klasöründeki migration'ı uygular ve `CariKartSiparisDb` veri tabanını tablolarıyla birlikte oluşturur. Ayrıca bir komut çalıştırmanıza gerek yoktur.

Elle oluşturmak isterseniz Package Manager Console üzerinden:

```powershell
Update-Database
```

🔒 Projenin yapısı aşağıda gösterilmiştir. İçeriğini klasör klasör inceleyebilirsiniz:

```
CariKartSiparisYonetimi/
├── Context/         Veri tabanı bağlamı ve ilişki tanımları
├── Controllers/     CariKartController, SiparisController, HomeController
├── Entities/        CariKart, Siparis, SiparisDetayi
├── Migrations/      EF Core migration dosyaları
├── Views/           Razor ekranları
└── wwwroot/         CSS, JavaScript ve yüklenen logolar
```

-----------------------------------------------------------------------

Görüşürüz 🎉