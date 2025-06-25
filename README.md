# 📚 Book Store API - .NET Core Backend

Bu proje, kitap ve kategori yönetimini içeren RESTful API altyapısına sahip bir kitap satış sistemidir. Modern yazılım geliştirme prensipleri (SOLID, DTO, Repository Pattern, AutoMapper, JWT) doğrultusunda geliştirilmiştir.

Frontend proje linki: https://github.com/aslisahin0/book-store-ui

---

## 🚀 Proje Özellikleri

- 📘 Kitap ve Kategori CRUD işlemleri (listeleme, ekleme, güncelleme, silme, detay)
- 🗂️ Katmanlı mimari: Application, Core, Infrastructure, Presentation
- ✅ Repository Pattern & Service Layer mimarisi
- 🔐 JWT Authorization
- 🔄 DTO & AutoMapper ile veri dönüşümü
- ⚠️ Global exception middleware ile hata yönetimi
- 📑 Swagger UI ile API dokümantasyonu
- 📑 xUnit ile Unit Test örneği

---

## 🛠️ Kurulum ve Çalıştırma Rehberi

### 1. Gerekli Yazılımlar

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/) (veya kendi veritabanınız)
- [DBeaver](https://dbeaver.io/) gibi bir veritabanı aracı

Kullanılan Başlıca NuGet Paketleri
```
Microsoft.EntityFrameworkCore	                         ORM
Npgsql.EntityFrameworkCore.PostgreSQL	                 PostgreSQL için EF Provider
AutoMapper                                             AutoMapper
Microsoft.AspNetCore.Authentication.JwtBearer	         JWT doğrulama
Swashbuckle.AspNetCore	                               Swagger arayüzü
xUnit	                                                 Unit test
```
---

### 2. Projeyi Klonla

```bash
git clone <proje-url>
cd book-store-api
```
---

### 3. Veritabanı Yapılandırması
appsettings.json içerisindeki DefaultConnection alanını kendi PostgreSQL bilgilerinize göre düzenleyin:
```
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=BookStoreDB;Username=postgres;Password=1234"
}
```

Ardından terminalde şu komutu çalıştırarak veritabanını oluşturun:
Migration Oluşturma (Sadece ilk kurulum için)
Visual Studio'da Tools > NuGet Package Manager > Package Manager Console menüsüne gidin ve şu komutları çalıştırın:
```
Add-Migration InitialCreate
Update-Database
```
Eğer migration daha önce oluşturulmuşsa yalnızca Update-Database yeterlidir.

---

### 4. Uygulamayı Başlat

Visual Studio üzerinden BookStore.API projesini seçin ve başlatın (F5 veya Ctrl+F5).
Uygulama aşağıdaki gibi bir endpoint üzerinden çalışır:
```
http://localhost:5092/swagger
```
Buradan tüm API uç noktalarını test edebilirsiniz.

---

### 📦  Proje Klasör Yapısı ve Katmanlı Mimari

BookStoreDemo/
│
├── Application/                    → DTO'lar ve Interface'ler
│   └── BookStore.Application/  
│       ├── DTOs/
│       └── Interfaces/
│           ├── Repository/
│           └── Service/
│       └── IUnitOfWork.cs

├── Core/
│   └── BookStore.Core/            → Entity tanımları
│       └── Entities/

│   └── BookStore.Library/         → Hata yönetimi, yardımcı sınıflar 
│       └──      

├── Infrastructure/                → EF Core, Repository,Servisler, DbContext
│   └── BookStore.Infrastructure/ 
│       ├── Data/
│       ├── Migrations/
│       ├── Profile/
│       ├── Repositories/
│       └── Services/

├── Presentation/                  → API katmanı (Controller, Middleware, JWT)

│   └── BookStoreDemo/
│       ├── Controllers/
 	 ├── Security/
│       ├── appsettings.json
│       ├── ExceptionHandlerMiddleware.cs
│       ├── Program.cs
│       └── BookStoreDemo.http

├── Tests/                         → Test Katmanı (xUnit ile Unit Testler)
│   └── BookStore.Tests/
│       └── Services/
│           └── BookServiceTests.cs

---

### 🔐 JWT Authentication
JWT ile giriş yapma ve yetkilendirme işlemleri sağlanır.
```
api/Auth/GetToken → Giriş yapıp token al

api/Auth/Validate → Token doğrulama
```
Token'ı aldıktan sonra korumalı endpoint'lere Authorization: Bearer <token> ile erişebilirsiniz.

---

### ⚙️ Örnek API Endpoint’leri
```
Metot	     Endpoint	                Açıklama
GET	    /api/Book/GetAll	        Tüm kitapları getirir
POST	  /api/Book/Create	        Yeni kitap ekler
PUT	    /api/Book/Update/{id}	    Kitap günceller
DELETE	/api/Book/Delete/{id} 	  Kitap siler
GET    	/api/Category/GetAll	    Kategori listesi
```
---

### 🧪 Unit Testleri Çalıştırma
Projede birim testler, BookStore.Tests projesi altında xUnit kullanılarak yazılmıştır.

### 1. Gerekli Paketler
Eğer test projesine ait NuGet paketleri yüklü değilse aşağıdaki komutlarla kurabilirsiniz:

```
dotnet add BookStore.Tests package xunit
dotnet add BookStore.Tests package xunit.runner.visualstudio
dotnet add BookStore.Tests package Microsoft.NET.Test.Sdk
```
---

### 2. Testleri Çalıştırma
Terminal üzerinden çalıştırmak için:

```
dotnet test
```
Bu komut tüm test projelerini tarar ve testleri çalıştırır. Sonuçlar terminalde görüntülenir.

Visual Studio üzerinden çalıştırmak için:

Test > Test Explorer menüsünü açın.

Tüm testleri listeleyin.

Sağ tıklayıp "Run All Tests" seçeneğini seçin.

---

### 📁 Test Klasörü Yapısı

BookStore.Tests/
│
└── Services/
    └── BookServiceTests.cs  → BookService’e ait bazı test senaryoları
    
Arrange → Act → Assert mantığıyla test senaryoları yapılandırılmıştır.

