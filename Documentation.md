# Proje Amacı
Product Management API, ürünlerle ilgili temel CRUD işlemlerini gerçekleştirmek ve bu işlemleri JWT ile korunan bir RESTful API üzerinden sunmak amacıyla geliştirilmiştir.

# Mimari
Bu proje, katmanlı mimari ve SOLID prensipleri dikkate alınarak geliştirilmiştir. Proje, API, Application, Persistence, Infrastructure ve Core katmanlarından oluşmaktadır. 
- **API**: İstemci isteklerini alır ve iş mantığını uygular.
- **Application**: İş mantığı ve veri transfer objeleri (DTO) burada bulunur.
- **Persistence**: Veritabanı erişimi Entity Framework Core aracılığıyla burada gerçekleştirilir.
- **Infrastructure**: Harici servisler (e-posta gönderimi, loglama gibi) burada yer alır.
- **Core**: Temel yapı taşları ve arayüzleri içerir.

# Kullanılan Teknolojiler
- .NET Core 6.0
- Entity Framework Core
- SQL Server
- JWT (JSON Web Token)
- AutoMapper
- FluentValidation
- Serilog
- Autofac
- MediatR
- Docker ve Docker Compose
- Swagger

# Kurulum ve Çalıştırma Talimatları
### Docker ile Kurulum
1. Depoyu klonlayın:
   ```bash
   git clone <repo-url>
   
2. Proje dizinine gidin:
cd ProductManagementAPI

3. Docker Compose ile uygulamayı başlatın:
docker-compose up

4. Tarayıcıda http://localhost:5000/swagger adresine giderek API'yi test edin.

##API Belgeleri
API belgelerine Swagger arayüzü üzerinden erişebilirsiniz. API'nin ana uç noktaları şunlardır:

- GET /api/products: Tüm ürünleri listeler.
- POST /api/products: Yeni bir ürün ekler (Yalnızca yetkilendirilmiş kullanıcılar).
- PUT /api/products/{id}: Mevcut bir ürünü günceller (Admin rolü gerektirir).
- DELETE /api/products/{id}: Belirli bir ürünü siler (Admin rolü gerektirir).
##Veritabanı Yapısı
Products: Ürün bilgilerini içeren ana tablo. Bu tablo, ürünlerin temel özelliklerini (örneğin, Id, Name, Price, Description, vb.) barındırır.

AspNetUsers: ASP.NET Identity tarafından oluşturulan ve kullanıcıların kimlik doğrulamasını ve yetkilendirmesini yönetmek için kullanılan tablo. Kullanıcıların benzersiz kimlik bilgilerini (UserName, Email, PasswordHash, vb.) içerir.

AspNetRoles: Kullanıcıların sahip olabileceği rolleri (Admin, User, vb.) tanımlayan tablo. Rol tablosu, kullanıcıların yetki seviyelerini kontrol etmek için kullanılır.

AspNetUserRoles: Kullanıcı ve rol ilişkisini temsil eder. Bu tablo, hangi kullanıcının hangi role sahip olduğunu gösterir.

AspNetUserClaims: Kullanıcılara atanan belirli hak taleplerini (claims) içerir. Bu hak talepleri, kullanıcıların yetkilerini daha ayrıntılı bir şekilde tanımlamak için kullanılır.

AspNetUserLogins: Harici sağlayıcılar (Google, Facebook, vb.) üzerinden yapılan kullanıcı girişlerini yönetir.

AspNetUserTokens: Kullanıcılar için üretilen kimlik doğrulama tokenlarını saklar.

Logs: Serilog ile yapılandırılmış, uygulama loglarının saklandığı tablo. Bu tablo, uygulama çalışması sırasında oluşan olayları (hatalar, bilgi mesajları, uyarılar, vb.) içerir.

##Kimlik Doğrulama ve Yetkilendirme
JWT kullanılarak kimlik doğrulama yapılmaktadır. Kullanıcılar, kayıt olduktan ve giriş yaptıktan sonra bir JWT token alırlar. Bu token, API'deki korumalı uç noktalara erişmek için kullanılır.

Örnek Kullanım
/api/auth/register uç noktasını kullanarak yeni bir kullanıcı kaydı oluşturun.
/api/auth/login uç noktasından bir JWT token alın.
Bu tokeni swaggerin Authorize kutucuğunu tıklayarak şu hiyerarşideki gibi yazın: Bearer JWTToken
Bu token ile /api/products uç noktasına ürün ekleyin.
