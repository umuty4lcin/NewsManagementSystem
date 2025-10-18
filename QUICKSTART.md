# HIZLI BAŞLANGIÇ REHBERİ

## Projeyi Çalıştırma

### 1. Terminalde Çalıştırma

```bash
cd C:\Users\YourUser\Desktop\NewsManagementSystem\NewsManagementSystem.Web
dotnet run
```

Uygulama başladıktan sonra tarayıcınızda şu adreslerden birine gidin:
- https://localhost:5001
- http://localhost:5000

### 2. Visual Studio ile Çalıştırma

1. `NewsManagementSystem.sln` dosyasını Visual Studio ile açın
2. F5 tuşuna basın veya "Start Debugging" butonuna tıklayın

## İlk Giriş

### Admin Paneline Giriş

1. Ana sayfada sağ üstteki **"Admin Girişi"** linkine tıklayın
2. Aşağıdaki bilgilerle giriş yapın:
   - **Email:** admin@haberportal.com  
   - **Şifre:** Admin123!

## Temel Kullanım

### Haber Ekleme

1. Admin Panel'de **"Haberler"** menüsüne tıklayın
2. **"Yeni Haber Ekle"** butonuna basın
3. Formu doldurun:
   - Başlık
   - Özet
   - İçerik
   - Yazar
   - Kategori seçin
   - Görsel yükleyin (opsiyonel)
4. **"Kaydet"** butonuna basın
5. Haberi yayınlamak için liste sayfasında yeşil göz ikonuna tıklayın

### Kategori Ekleme

1. **"Kategoriler"** menüsüne tıklayın
2. **"Yeni Kategori Ekle"** butonuna basın
3. Kategori adı ve açıklama girin
4. Kaydet

### Kullanıcı Ekleme (Sadece Admin)

1. **"Kullanıcılar"** menüsüne tıklayın
2. **"Yeni Kullanıcı Ekle"** butonuna basın
3. Kullanıcı bilgilerini girin
4. Rol seçin (Admin veya Editor)
5. Kaydet

## Varsayılan Veriler

### Kategoriler
Sistem otomatik olarak şu kategorileri oluşturur:
- Genel
- Teknoloji
- Spor
- Ekonomi
- Sağlık
- Eğitim

### Roller
- **Admin:** Tüm yetkilere sahip
- **Editor:** Haber ve kategori yönetimi (kullanıcı yönetimi yok)

## Önemli Notlar

1. **Veritabanı:** İlk çalıştırmada otomatik oluşturulur
2. **Resimler:** `wwwroot/uploads/news/` klasörüne kaydedilir
3. **Soft Delete:** Silinen veriler fiziksel olarak silinmez, IsActive=false yapılır
4. **Şifre Politikası:** 
   - Minimum 6 karakter
   - En az 1 büyük harf
   - En az 1 küçük harf
   - En az 1 rakam

## Sorun Çözümleri

### "Database connection error"
- SQL Server veya LocalDB'nin çalıştığından emin olun
- Connection string'i kontrol edin

### "Migration pending"
```bash
cd NewsManagementSystem.Web
dotnet ef database update --project ../NewsManagementSystem.Data
```

### "Port already in use"
Farklı bir port kullanmak için:
```bash
dotnet run --urls="https://localhost:5555;http://localhost:5556"
```

## Proje Durumu

✅ **Tamamlanan Özellikler:**
- Katmanlı mimari yapısı
- Entity modelleri
- Repository Pattern
- Unit of Work
- Identity authentication
- Admin Panel
  - Login/Logout
  - Şifremi Unuttum
  - Dashboard
  - Haber yönetimi (CRUD)
  - Kategori yönetimi (CRUD)
  - Kullanıcı yönetimi (CRUD)
- Frontend
  - Haber listesi
  - Haber detay
  - Kategori filtreleme
- Database migration ve seed data

⏳ **Gelecek Geliştirmeler:**
- Porto Admin Template entegrasyonu
- Pagination
- Arama fonksiyonu
- Email servisi (şifre sıfırlama için)

## Test Senaryosu

1. ✅ Uygulamayı başlatın
2. ✅ Ana sayfayı görüntüleyin (henüz haber yok)
3. ✅ Admin paneline giriş yapın
4. ✅ Dashboard'u kontrol edin
5. ✅ Yeni kategori ekleyin
6. ✅ Yeni haber ekleyin
7. ✅ Haberi yayınlayın
8. ✅ Ana sayfaya dönün ve haberi görün
9. ✅ Haber detayına tıklayın
10. ✅ Kategori filtrelemeyi test edin

## Destek

Herhangi bir sorun yaşarsanız:
1. README.md dosyasını okuyun
2. Build log'larını kontrol edin
3. Database bağlantısını kontrol edin

---

**Geliştirme Süresi:** 4 gün için tasarlandı  
**Framework:** ASP.NET Core 9.0  
**Veritabanı:** MS-SQL LocalDB  

