using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlternetSiparisYazilimi.Models;
using GirisProjesi2.Models;
using Microsoft.AspNetCore.Mvc;
using GirisProjesi2.Altyapi;

namespace GirisProjesi2.Controllers
{
    public class AnaSayfaController : Controller
    {
        /*Bu örnek projede ASP.NET CORE MVC'de sık kullanılan birçoğu 
        yeni C# özellikleriyle staj projesine hazırlık olması açısından pratik yapıyorum.*/
        public ViewResult Index()
        {
            //return View(new string[] { "Tayfun", "PAÜ Bilgisayar Mühendisliği/C.E.", "Şevket", "Türkiye" });

            //?. null şartlı atama/conditinonal operatörü
            List<string> urunListesi = new List<string>();
            foreach (Urun u in Urun.Urunler())
            {
                string isim = u?.Isim; // null conditional operatörü(?.)  yani mesela u değişkeni null ise derleyici null referansı istisna/exception hatası vermez. direk atamayı null olarak yapar(önce İsmi null olarak atar sonra da bu değeri en baştakine atar).. null değil ise İsim'in değeri ne ise onu atar.
                decimal? fiyat = u?.Fiyat; // ?. zincir şeklinde de uygulanabilir. xxx?.yyy?.zzz gibi
                string kategori = u?.Kategorisi;
                urunListesi.Add(string.Format("Ürün Adı: {0}, Fiyatı: {1}, Kategorisi:{2}", isim, fiyat, kategori));
            }
            //string.Format ile $ string interpolasyon operatörü aynı görevi görür.

            //null birleştirici/coeslecing operatörü(??)
            List<string> urunListesi2 = new List<string>();
            foreach (Urun u in Urun.Urunler())
            {
                string isim = u?.Isim ?? "İsim değeri atanmamış"; //Null birlştirici/coalescing operatörü ?? dir. Ve bir başarısızlık olursa veya isim değeri null ise "İsim değeri atanmamış" olarak atama yapılır.
                decimal? fiyat = u?.Fiyat ?? 0; // değer u veya fiyat null ise ?? null birleştirici operatörü 0 değeri atanır.
                string kategori = u?.Kategorisi ?? "Kategorisi yok";
                urunListesi2.Add(string.Format("Ürün Adı: {0}, Fiyatı: {1}, Kategorisi:{2}, Ürün Resmi(Null Gösterimi İçin):{3}", isim, fiyat, kategori, null));
            }
            //string.Format ile $ string interpolasyon operatörü aynı görevi görür.

            //$ string interpolasyon operatörü, string.format'ın daha kolay hali kısaca.
            List<string> urunListesi3 = new List<string>();
            foreach (Urun u in Urun.Urunler())
            {
                string isim = u?.Isim ?? "İsim değeri atanmamış"; //Null birlştirici/coalescing operatörü ?? dir. Ve bir başarısızlık olursa veya isim değeri null ise "İsim değeri atanmamış" olarak atama yapılır.
                decimal? fiyat = u?.Fiyat ?? 0; // değer u veya fiyat null ise ?? null birleştirici operatörü 0 değeri atanır.
                string kategori = u?.Kategorisi ?? "Kategorisi yok";
                urunListesi3.Add($"Ürün Adı: {isim}, Fiyatı: {fiyat}, Kategorisi:{kategori}, Ürün Resmi(Null Gösterimi İçin):{kategori}");
            }
            //string.Format ile $ string interpolasyon operatörü aynı görevi görür.

            //obje başlatıcı/initializer
            Urun urunum = new Urun { Isim = "Bu bir otomatik obje başlatıcı", Fiyat = 0, Aciklama = "Harika bir şey" };
            //Normalde new Urun() şeklinde constructor çağrılı daha sonra tek tek atama yapılır. Ancak object başlatıcı işimizi kolaylaştırır.

            //colleciton başlatıcı/initializer
            string[] koleksiyonum = new string[] { "paü", "deü", "harvard", "stanford" };
            IEnumerable<Urun> yeniKoleksiyon = new Urun[] { new Urun { Isim = "X" }, new Urun { Isim = "Y" } };
            Dictionary<int, Urun> sozluk = new Dictionary<int, Urun> { { 1, new Urun { Isim = "Aspirin" } }, { 2, new Urun { Isim = "Mazejik" } } };
            Dictionary<int, Urun> sozlukYeniCSharp = new Dictionary<int, Urun> { [1] = new Urun { Isim = "X" } , [2] = new Urun { Isim = "Y" }};
            //tek tek koleksinum[x] şeklinde atama yapmaktans bu şekilde kolayca atama yaptım. derleyici kendisi boyutu hesaplar

            //return View(sozlukYeniCSharp.Values.Select(U => U?.Isim));

            //Extension metodunu kullanmak için mesela burada(kullanacağımız yerde) using ile sınıfa dahil etmemiz gerekiyor.
            Sepet s = new Sepet { SepetUrunleri = Urun.Urunler() };
            decimal toplamFiyat = s.ToplamFiyatiHesapla();


            IEnumerable<Urun> dahaPahaliOlanlar = s.FiltreleDahaPahali(100M);
            IEnumerable<Urun> dahaUcuzOlanlar = s.FiltreleDahaUcuz(1000M);

            //Bu bir aday fonksiyon, bunu parametre olarak geçirebiliriz.
            Func<Urun, bool> adayFonk = delegate (Urun u) { return u?.Fiyat > 10 & u?.Fiyat < 100; };

            //Delegate(aday fonksiyon yöntemi eskiden kullanılırdı.
            IEnumerable<Urun> filtreleDelegate = s.FiltreleLambdali(adayFonk); // 10 ile 100 arasındakileri istedik.
            //Yeni C# Lambda ile parametre olarak fonksiyon göndererek ile daha hızlı işlem yapmayı sağlar.
            IEnumerable<Urun> filtreleLambdali = s.FiltreleLambdali(u => u?.Fiyat > 10 & u?.Fiyat < 100); // 10 ile 100 arasındakileri istedik.

            //burada type(tip) çıkarımı var ile sağlanmış
            //ayrıca hızlı object başlatıcı(construction ve property atama vb. olaylar) "{" kullanmıştır
            var anonim = new[] { new { isim = "a",degeri=22.3M },new { isim = "b", degeri = 12M } };
            string[] str = anonim.Select(anonimOgesi => anonimOgesi.isim).ToArray(); // lambda ifadeleri "isim" dizisini alacak şekilde dönüşüm sağlayabiliriz.

            return View(filtreleLambdali.Select(u => u.Isim)); // isim'leri string dizi olarak döndürüyorum       
           
            
        }
        public async Task<string> BoyutHesapla()
        {
            long? boyutByte = await Async_Await_Kullanimi.SayfaBoyutuHesaplaYeniCSharp();
            return "paü.edu.tr'nin " + boyutByte.ToString() + " byte sayfa büyüklüğü bulunmaktadır.";
        }

    }
}