  # RestaurantProject

*NTier katmanlı mimariye sahip bir projedir.

  ## Entity Layer

-CodeFirst yaklaşımıyla veri tabanında tablo haline getirilecek sınıflarımız bu katmanda yer alacaktır.

** "Product","Category","Customer","Employee","Reservation","Supplier","Order",
"TableOfRestaurant","Ingredient","ProductIngredient","Invoice","Kitchen","AccountingTransaction"

-Data Transfer Objects ve View Models dosyaları da bu katmanda yer almaktadır.

  ## DAL (Data Access Layer)

- Proje Context sınıfımız bu katmanda yer almaktadır ve veri tabanı nesnesini temsil etmektedir.

#Kurulu olan paketlerimiz şunlardır;

- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools

  ## BLL (Business Logic Layer)

-AbstractRepositories
  - IRepository: Oluşturmuş olduğumuz entityler için  gerçekleştirelecek eylemleri soyut olarak dahil ettiğimiz interface kontratımızı temsil eder. Bu interface dışarıdan T tipinde bir BaseEntity almalıdır. 
 -Repositories
   - BaseRepository: Bu class IRepository interface'i üzerinden implement edilen somut bir class'dır. Interface içerisinde tanımlı olan eylemler burada bütün entity'ler için somut olarak oluşturuldu. Bunun mantığı ise BaseEntity tipinde olan bütün varlıklara uygulanacak olan ortak eylemleri içermesidir. (Create, Read, Update, Delete)
- AbstractServices
	*Entityler için özel oluşturulacak soyut sınıflar burada yer almaktadır.Bu sınıflar IRepository interface i implement etmektedir.Temel CRUD eylemleri dışında da entity için farklı eylemlemler gerçekleştirmek isteyebiliriz. Burada soyut olarak entity e özel eylemlerimizi tanımlayabiliriz.
        
- Services
	-BaseService:BaseService<T> BaseEntity tipinde T alması zorunlu olan IService<T> interface i implement eder. Constructor'da IRepository<T> tip alması belirtilerek bu servisin IRepository bağımlılığı gerçekleşmiştir.Böylelikle BaseService'in alacağı herhangi BaseEntity T tipi IRepository'e aynı tip ile bağımlı olacaktır.
         *Bir örnek olarak:  ProductService sınıfı şu şekilde; ProductService : BaseService<Product>,IProductService implement edilerek bağımlılıklar entity e özel olarak belirlenmiş olacaktır. Bu işlemle beraber yalnızca Product entity e özel işlemler varsa bu servise dahil edilerek gerçekleştirebiliriz. Bu işlem her entity için ayrı servis olarak gerçekleştirilerek sorumluluklar ayrıştırılır.
    
 #Kurulu olan paketlerimiz şunlardır;
  - AutoMapper.Extensions.Microsoft.DependencyInjection
  - FluentValidation.AspNetCore

- Mapping
  -MapProfile clasımız AutoMapper ile gelen Profile sınıfından miras alır.Böylelikle CreateMap metotu yardımıyla kaynak ve hedef arasında eşleşme gerçekleştirebiliriz. Dilersek bunu tersine çevirerek hedef sınıftan kaynağa yönlü eşleşmede gerçekleştirmemize olanak sağlanır. Data Transfer Objects ve View Model lerimizi  AutoMapper yardımıyla entity lerimizle eşleştirerek birçok avantaj sağlarız.
*Kod maliyeti azaltma,kurala dayalı eşleşme sağlama,bağımlılık olarak eklenerek kullanma ve test edilebilirliği arttırma,kaynaklarda yapılacak değişiklikte haritalama mantığıyla daha hızlı ve kolay şekilde güncellemeler gerçekleştirmeyi sağlamak gibi avantajlar vardır.

- FluentValidationService
      -IValidation interface ile beraber ValidationService sınıfına rehber oluştururak T tipinde nesne almasını sağladık. ValidationService Constructor da FluentValidation kütüphanesiyle gelen IValidator<T> interface ine sahip olduğu T tipini gönderek bağımlı hale gelir.Controller üzerinde dependency injection edeceğimiz IValidation<T> interface i generic bir yapıyla beraber her bir View Model için uygulanabilir hale gelerek tekrar edecek kodlardan kurtulmamıza yardımcı olur.Dahası farklı View Model veya türler için farklı IValidation<T> bağımlılıkları eklememize olanak vererek esneklik sağlamış olur.
**Fluent Validation karmaşık tanımlamalarından kurtarır ve ayrı doğrulama kurallarını ayrı sınıflarda tanımlamamıza izin vererek temiz bir kod yazmamıza yardımcı olur.Güncellemek veya silmek istediğimiz kodlara kolayca ulaşabilir hızlı bir şekilde değiklik yapabiliriz.

  ## IOC
 - Bu katman Service içerisinde kullanılacak olan Repository'leri ve Somut sınıfları dahil eden bir static metot barındırmaktadır. Bu metot Program.cs içerisinde arakatmada modüler olarak dahil edilmiştir. Amacımız; proje dahilinde kullanmak istediğimiz service'leri tek bir çatı altında toplayarak dilediğimizde değiştirme imkanına sahip olabilmemiz içindir.

## Api
 Get,Post,Put eylemleriyle MVC katmanında test edilerek kullanıma sokuldu.

## MVC (Presentation Layer)

- Temel controller içerisinde proje konusu olan Restoranımızın tanıtım sunumu için web site templete i,kayıt ve kullanıcı girişi işlemleriyle beraber rezervasyon eylemleri gerçekleştirildi.
- Area içerisinde Yönetici ve çalışanlar için oluşturulan Manager Dashboard'u ile beraber tüm restoran iç hizmetleri ayarlanmıştır.
Rezervasyon oluşturma,Sipariş oluşturma,Sipariş takip,Stok takip,Menu işlemleri,Ürün işlemleri,Hesap ve kasiyer işlemleri,Muhasebe paneli,Tedarikçi,borç-alacak takibi ve fatura işlemleri gibi gerekli hizmetler sağlanmıştır.
