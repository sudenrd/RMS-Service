# Modbus Flow Computer Simülasyonu ve Veri Toplama Servisi

Bu proje, Modbus TCP üzerinden endüstriyel Flow Computer verilerini simüle eden, arka planda asenkron olarak toplayan ve bir Web API aracılığıyla sunan bir sistemdir. Backend tarafında veri güvenilirliğini ve sürdürülebilirliği sağlamak için Onion Architecture prensipleri kullanılmıştır.

## 🛠️ Kullanılan Teknolojiler ve Kütüphaneler

**Backend (.NET / C#)**
* **.NET 8.0 SDK** - Modern, güvenli ve yüksek performanslı sunucu altyapısı.
* **Worker Service** - Arka planda kesintisiz çalışan, Modbus sunucusunu periyodik olarak dinleyen servis mimarisi.
* **ASP.NET Core Web API** - Veritabanına kaydedilen ölçümlerin dış sistemlere ve arayüzlere açılmasını sağlayan RESTful API. Swagger UI ile interaktif test imkanı sunar.
* **Entity Framework Core & MySQL** - Code-First yaklaşımıyla veritabanı şemalarının oluşturulması ve yüksek performanslı CRUD işlemleri.
* **NModbus** - Endüstriyel donanımlarla iletişim kurmak için Modbus TCP protokolü üzerinden Master/Client bağlantısının kurulması ve register adreslerinin okunması.

**Flow Computer Simülatörü (Python)**
* **PyModbus** - Sensör verilerini ağ üzerinden dışarıya açan asenkron Modbus TCP Sunucusu (Server).
* **Threading** - Veri üretim döngüsünün ve Modbus sunucusunun eşzamanlı, ana programı bloklamadan stabil çalışmasını sağlayan yapı.
* **Struct (Data Packing)** - Üretilen float (ondalıklı) verilerin IEEE 754 standardına uygun olarak Big-Endian 16-bit byte dizilerine dönüştürülmesi.

## ⚙️ Kurulum ve Çalıştırma

1. **Simülasyon:** Python dizininde bağımlılıkları yükledikten sonra `main.py` dosyasını çalıştırarak TCP sunucusunu dinlemeye alın (Varsayılan: 127.0.0.1:5020).
2. **Konfigürasyon:** `Presentation/WorkerService` ve `Presentation/WebApplication` içerisindeki `appsettings.json` dosyalarına kendi MySQL Connection String bilginizi girin.
3. **Servisi Başlatma:** .NET Solution'ı ayağa kaldırdığınızda Worker servis verileri Modbus üzerinden toplayıp veritabanına kaydetmeye başlayacak, Web API üzerinden ise bu kayıtlara erişilebilecektir.


## 📂 Proje Dizin Yapısı

```text
RMS_System/
│
├── Backend/                            # .NET Çözümü (.sln) - Temiz Mimari
│   ├── Core/
│   │   ├── Domain/                     # Varlıklar (Entities) ve İş Modelleri (FC)
│   │   └── Application/                # Arayüzler (Interfaces), Repository Sözleşmeleri
│   │
│   ├── Infrastructure/
│   │   ├── Persistence/                # Entity Framework Core, MySQL veritabanı işlemleri
│   │   └── External_Service/           # Modbus TCP haberleşme ve veri okuma (NModbus)
│   │
│   └── Presentation/
│       ├── WorkerService/              # Periyodik olarak sahayı yoklayan arka plan servisi
│       └── WebApplication/             # Toplanan verileri sunan Swagger Web API
│
└── FlowComputer/                       # Python Modbus TCP Simülatörü
    ├── main.py                         # Ana simülasyon döngüsü ve Modbus sunucusu
    ├── config.json                     # Sensör parametreleri ve register yapılandırması
    ├── ModbusKurulum.py                # Float değerleri 16-bit (Big-Endian) formata dönüştürme
    ├── RegisterManager.py              # Sensör register haritasına yazma işlemleri
    ├── VeriUretimSimulasyonu.py        # Akış, basınç, sıcaklık ve enerji verisi simülasyonu
    └── requirements.txt                # Python bağımlılıkları

