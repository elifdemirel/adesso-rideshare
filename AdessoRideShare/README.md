# Adesso Ride Share API

Bu proje, kullanıcıların seyahat planları oluşturup yayınlayabildiği ve diğer kullanıcıların bu planları şehir bazlı veya güzergâh bazlı filtreleyerek arayabildiği bir **ASP.NET Core Web API** uygulamasıdır.

## Temel Özellikler

- Kullanıcılar kalkış ve varış şehirlerini belirterek seyahat planı oluşturabilir.
- Yayınlama ve yayından kaldırma işlemleri yapılabilir.
- Kullanıcılar doğrudan bir şehirden diğerine giden planları arayabilir.
- Ayrıca sistem, bir şehirden başka bir şehre giderken güzergâhtan geçen planları da bulabilir.

## Kurulum Adımları

```bash
# Projeyi klonlayın
git clone https://github.com/elifdemirel/adesso-rideshare.git

# Proje klasörüne girin
cd adesso-rideshare

# Gerekli bağımlılıkları yükleyin
dotnet restore

# Veritabanını güncelleyin
dotnet ef database update

# Uygulamayı başlatın
dotnet run
API Endpointleri
HTTP Metodu	Endpoint	Açıklama
POST	/api/travelplans	Yeni bir seyahat planı oluşturur
PUT	/api/travelplans/{id}/publish?publish=true	Planı yayına alır veya yayından kaldırır
GET	/api/travelplans/search?fromCityId=1&toCityId=2	Kalkış-varış şehirlerine göre planları listeler
GET	/api/travelplans/passingthrough?fromCityId=1&toCityId=7	Güzergâhtan geçen planları listeler

Örnek Test Senaryoları
Test Case 1: Manisa → Eskişehir Planı
Şehir ID’leri:

Manisa: 4

Eskişehir: 7

Mevcut Plan: Manisa’dan Eskişehir’e doğrudan plan mevcuttur.

Test:
GET /api/travelplans/passingthrough?fromCityId=1&toCityId=7

Beklenen: Bu plan, 1 → 7 güzergâhı üzerinde bulunduğu için listelenmelidir.

Sonuç: Artık doğru şekilde dönmektedir.

Test Case 2: İzmir → Ankara Planı
Şehir ID’leri:

İzmir: 1

Ankara: 2

Mevcut Plan: Doğrudan İzmir → Ankara planı vardır.

Test:
GET /api/travelplans/search?fromCityId=1&toCityId=2

Beklenen: Doğrudan plan dönmelidir.

Rota Algoritması Açıklaması
Sistem, şehirleri X ve Y koordinatlarına göre 20x10 luk bir grid üzerinde değerlendirir.

Rota hesaplamasında Breadth-First Search (BFS) algoritması kullanılır.

Komşu şehirler, aralarındaki grid mesafesi 2.5 birimden küçükse bağlı kabul edilir.

Örnek Şehir Koordinatları
Şehir	GridX	GridY
İzmir	    2	8
Manisa	    3	7
Ankara	    10	5
Eskişehir	9	4

Swagger Kullanımı
Tüm API uç noktalarını görsel olarak test etmek için Swagger arayüzüne şu adresten erişebilirsiniz:

http://localhost:{port}/swagger
Geliştirici Notları
Veriler HasData metodu ile OnModelCreating içinde veritabanına tohumlanmıştır.

Test kullanıcı kimliği sabit olarak kullanılmıştır:

aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa