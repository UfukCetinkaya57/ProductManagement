# Product Management API

Bu proje, ürün yönetimi için geliştirilmiş bir .NET Core Web API uygulamasıdır. Proje, modern yazılım geliştirme tekniklerini ve mimarilerini kullanarak, ürünlerin eklenmesi, güncellenmesi, silinmesi ve listelenmesi gibi temel CRUD işlemlerini gerçekleştirmektedir. Ayrıca, JWT (JSON Web Token) ile kimlik doğrulama ve rol tabanlı erişim kontrolü sağlar.

## Proje Özeti

Product Management API, kullanıcıların ürünlerle ilgili CRUD işlemlerini yapabilmelerini sağlayan bir web servisidir. Bu servis, kimlik doğrulama ve yetkilendirme işlemlerini de içerir. Admin rolüne sahip kullanıcılar ürünleri güncelleyebilir ve silebilirken, yetkisiz kullanıcılar sadece ürünleri görebilir ve ekleyebilir.

## Kullanılan Teknolojiler

- **.NET Core 6.0**: Projenin temel framework'ü olarak kullanılmıştır. .NET Core, yüksek performanslı, açık kaynak ve çapraz platform bir framework'tür.
- **Entity Framework Core**: Veritabanı işlemlerini gerçekleştirmek için kullanılan ORM (Object-Relational Mapper) aracıdır. Bu projede Code-First yaklaşımı benimsenmiştir.
- **SQL Server**: Projenin veritabanı olarak Microsoft SQL Server kullanılmıştır.
- **JWT (JSON Web Token)**: Kimlik doğrulama ve yetkilendirme işlemleri için kullanılmıştır. JWT, kullanıcıların güvenli bir şekilde sisteme giriş yapmasını ve belirli kaynaklara erişmesini sağlar.
- **Dependency Injection**: Projede bağımlılıkların yönetimi için kullanılmıştır. Bu, kodun daha modüler ve test edilebilir olmasını sağlar.
- **AutoMapper**: Domain modelleri ile DTO'lar arasında otomatik eşleme yapmak için kullanılmıştır.
- **FluentValidation**: Kullanıcı girişlerinin doğrulanması için kullanılan bir kütüphanedir.
- **Serilog**: Uygulama içinde yapılandırılmış loglama yapmak ve logları farklı hedeflere (konsol, dosya, veritabanı) göndermek için kullanılmıştır.
- **Autofac**: Dependency Injection (bağımlılık enjeksiyonu) için kullanılmıştır. Daha modüler ve genişletilebilir bir DI altyapısı sağlar.
- **MediatR**: CQRS (Command Query Responsibility Segregation) mimarisi ile komut ve sorgu işlemlerini ayırmak için kullanılmıştır.
- **Docker**: Uygulamanın container ortamında çalıştırılması için kullanılmıştır. Docker, uygulamanın farklı ortamlarda tutarlı bir şekilde çalışmasını sağlar.
- **Docker Compose**: Uygulamayı ve SQL Server veritabanını aynı anda çalıştırmak için Docker Compose kullanılmıştır.
- **Swagger**: API uç noktalarını belgeler ve test etmek için kullanıcı dostu bir arayüz sağlar.

## Proje Mimarisi

Proje, katmanlı mimari yaklaşımıyla geliştirilmiştir. Her katman belirli bir sorumluluk alanına sahiptir:

- **API Katmanı**: İstemcilerden gelen HTTP isteklerini alır ve işleme koyar. Bu katman, kimlik doğrulama ve yetkilendirme işlemlerini de içerir.
- **Application Katmanı**: İş kurallarının ve iş mantığının bulunduğu katmandır. DTO'lar, command ve query işlemleri bu katmanda bulunur.
- **Persistence Katmanı**: Veritabanı erişimi bu katmanda gerçekleştirilir. Entity Framework Core kullanılarak veritabanı işlemleri yapılır.
- **Infrastructure Katmanı**: Projede kullanılacak servislerin (örneğin, loglama, e-posta servisi) bulunduğu katmandır.
- **Core Katmanı**: Projenin temel yapı taşlarını (örneğin, entity modelleri, arayüzler) içerir.

## Kurulum ve Docker ile Çalıştırma

### Gereksinimler
- Docker ve Docker Compose yüklü olmalıdır.

### Adım 1: Bu Depoyu Klonlayın
```bash 


Adım 2: Proje Dizinine Gidin

Kodu kopyala
cd ProductManagementAPI
Adım 3: Docker Compose ile Uygulamayı Başlatın
Aşağıdaki komut ile uygulamayı ve veritabanını container içinde başlatın:

docker-compose up --build
Adım 4: Uygulamayı Test Edin
Docker Compose, hem uygulama hem de SQL Server veritabanını ayrı container'larda çalıştıracaktır. Uygulamanın başarıyla çalışıp çalışmadığını test etmek için tarayıcınızı açın ve aşağıdaki URL'ye gidin:

http://localhost:5000/swagger
Bu, Swagger arayüzünü açacak ve API uç noktalarını test etmenizi sağlayacaktır.
```
## Kullanım
Uygulama, ürünlerle ilgili CRUD işlemlerini ve JWT ile korunan kimlik doğrulama uç noktalarını içerir. Swagger arayüzünü kullanarak API'yi test edebilirsiniz.

##Kullanıcı Oluşturma ve Kimlik Doğrulama
/api/auth/register uç noktasını kullanarak bir kullanıcı kaydı oluşturun.
/api/auth/login uç noktasını kullanarak JWT token alın.
JWT token'ı kullanarak ürün ekleme, güncelleme ve silme işlemlerini gerçekleştirin.

##Önemli Komutlar
Docker Container'larını Durdurmak
Uygulamayı ve veritabanını durdurmak için:

docker-compose down
