# Haber Yönetim Sistemi

ASP.NET Core MVC ile geliştirilmiş katmanlı mimari kullanılarak oluşturulmuş bir haber yönetim sistemidir.

## Proje Yapısı

Proje 4 katmanlı mimari ile geliştirilmiştir:

### 1. NewsManagementSystem.Core
- Entity sınıfları (News, Category, AppUser)
- Interface tanımlamaları (Repository, Service)
- İş mantığı bağımsız katman

### 2. NewsManagementSystem.Data
- DbContext (AppDbContext)
- Repository implementasyonları
- Unit of Work pattern
- Entity Framework Core migrations

### 3. NewsManagementSystem.Service
- Business Logic katmanı
- Service implementasyonları
- İş kuralları

### 4. NewsManagementSystem.Web
- MVC Web uygulaması
- Admin Panel (Area)
- Frontend sayfaları
- Controllers ve Views

## Teknolojiler

- **Framework:** ASP.NET Core 9.0
- **ORM:** Entity Framework Core 9.0
- **Veritabanı:** MS-SQL (LocalDB)
- **Authentication:** ASP.NET Core Identity
- **Frontend:** Bootstrap 5, Font Awesome
- **Mimari:** Layered Architecture, Repository Pattern, Unit of Work

## Özellikler

### Frontend
- ✅ Haber listesi sayfası
- ✅ Haber detay sayfası
- ✅ Kategori bazlı filtreleme
- ✅ Responsive tasarım

### Admin Panel
- ✅ Login / Logout
- ✅ Şifremi Unuttum
- ✅ Dashboard (İstatistikler)
- ✅ Haber Yönetimi (CRUD)
  - Haber ekleme
  - Haber düzenleme
  - Haber silme (Soft Delete)
  - Haber yayınlama/yayından kaldırma
  - Resim yükleme
- ✅ Kategori Yönetimi (CRUD)
- ✅ Kullanıcı Yönetimi (CRUD) - Sadece Admin
- ✅ Role-based Authorization

## Kurulum

### Gereksinimler
- .NET 9.0 SDK
- SQL Server veya LocalDB
- Visual Studio 2022 veya VS Code

### Adımlar

1. **Projeyi Klonlayın veya İndirin**
   ```bash
   cd NewsManagementSystem
   ```

2. **Bağlantı Dizesini Kontrol Edin**
   
   `NewsManagementSystem.Web/appsettings.json` dosyasındaki connection string'i kontrol edin:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=NewsManagementDb;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

3. **Veritabanını Oluşturun**
   
   Veritabanı ilk çalıştırmada otomatik oluşturulacaktır. Manuel oluşturmak isterseniz:
   ```bash
   cd NewsManagementSystem.Web
   dotnet ef database update --project ../NewsManagementSystem.Data/NewsManagementSystem.Data.csproj
   ```

4. **Uygulamayı Çalıştırın**
   ```bash
   cd NewsManagementSystem.Web
   dotnet run
   ```

5. **Tarayıcıda Açın**
   
   Uygulama `https://localhost:5001` veya `http://localhost:5000` adresinde çalışacaktır.

## Varsayılan Kullanıcı

İlk kurulumda otomatik olarak bir admin kullanıcısı oluşturulur:

- **Email:** admin@haberportal.com
- **Şifre:** Admin123!

## Varsayılan Kategoriler

Sistem ilk çalıştırıldığında şu kategoriler otomatik oluşturulur:
- Genel
- Teknoloji
- Spor
- Ekonomi
- Sağlık
- Eğitim

## Kullanım

### Admin Paneline Giriş

1. Ana sayfadaki "Admin Girişi" linkine tıklayın
2. Yukarıdaki varsayılan kullanıcı bilgileri ile giriş yapın
3. Dashboard'dan işlemlerinizi gerçekleştirin

### Haber Ekleme

1. Admin Panel > Haberler > Yeni Haber Ekle
2. Başlık, özet, içerik ve kategori bilgilerini girin
3. İsteğe bağlı haber görseli yükleyin
4. Kaydet butonuna tıklayın
5. Haberi yayınlamak için "Yayınla" butonuna tıklayın

### Kullanıcı Ekleme (Sadece Admin)

1. Admin Panel > Kullanıcılar > Yeni Kullanıcı Ekle
2. Kullanıcı bilgilerini girin
3. Rol seçin (Admin veya Editor)
4. Kaydet

## Roller ve Yetkiler

### Admin
- Tüm işlemleri gerçekleştirebilir
- Kullanıcı yönetimi yapabilir
- Haber, kategori yönetimi

### Editor
- Haber ekleme, düzenleme, silme
- Kategori yönetimi
- Kullanıcı yönetimi yapamaz

## Veritabanı Şeması

### Tables
- **AspNetUsers** - Kullanıcılar (Identity)
- **AspNetRoles** - Roller (Identity)
- **News** - Haberler
- **Categories** - Kategoriler

### İlişkiler
- News -> Category (Many to One)
- News -> User (Many to One)

## Proje Klasör Yapısı

```
NewsManagementSystem/
├── NewsManagementSystem.Core/
│   ├── Entities/
│   └── Interfaces/
├── NewsManagementSystem.Data/
│   ├── Context/
│   ├── Repositories/
│   ├── Seed/
│   └── Migrations/
├── NewsManagementSystem.Service/
│   ├── Interfaces/
│   └── Services/
└── NewsManagementSystem.Web/
    ├── Areas/
    │   └── Admin/
    │       ├── Controllers/
    │       ├── Models/
    │       └── Views/
    ├── Controllers/
    ├── Views/
    └── wwwroot/
```

## Geliştirme Notları

### Eklenebilecek Özellikler (TODO)

- [ ] Porto Admin Template entegrasyonu (gelişmiş admin panel tasarımı)
- [ ] Resim optimizasyonu ve thumbnail oluşturma
- [ ] Pagination (sayfalama)
- [ ] Arama fonksiyonu
- [ ] Yorumlar
- [ ] Etiketler (Tags)
- [ ] Email bildirimleri (şifre sıfırlama için)
- [ ] Haber istatistikleri ve raporlar
- [ ] Cache mekanizması
- [ ] API endpoint'leri

### Güvenlik

- Identity kullanılarak kimlik doğrulama
- Role-based authorization
- Soft delete (veriler fiziksel olarak silinmez)
- Anti-forgery token koruması
- Password policy (min 6 karakter, büyük/küçük harf, rakam)

## Sorun Giderme

### Migration Hatası
```bash
dotnet ef migrations remove --project NewsManagementSystem.Data
dotnet ef migrations add InitialCreate --project NewsManagementSystem.Data
dotnet ef database update --project NewsManagementSystem.Data
```

### LocalDB Bağlantı Hatası
SQL Server Management Studio ile LocalDB'ye bağlanın ve veritabanını kontrol edin.


