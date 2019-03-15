using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlternetSiparisYazilimi.Models
{
    public class Urun
    {
        public Urun()
        {
            IsimSadeceOku = "Bu read-only özellik sadece constructor içinde bir kez atanabilir, değeri değişmez";
        }
        public int UrunID { get; set; }

        public string Isim { get; set; } // C# propp ve propfull + double TAB yaparak kısa yolla bu özellikleri Visual Studio'da oluşturabiliriz
        // Bu isim otomatik uygulanan bir özellik(autometically implemented propery)
        // ve aşağıdaki açık uygulanmış özelliğe "IsimExplicit" muhtemelen compiler time'de dönüştürülür.


        private string isimExplicit;

        public string IsimExplicit // Bu bir açıkça implemente edilen/uygulanan bir özellik 
        {
            get { return isimExplicit; }
            set { isimExplicit = value; }
        }

        public string Isim2 { get; set; } = "Tayfun"; // şeklinde otomatik property'ye atama yapılabilir.

        public string IsimSadeceOku{ get;} // bu bir read-only özellik. ancak bu Ürün modelinin constructor'ında atama yapılabilir. ve daha sonra değiştirilemez.
        public string Aciklama { get; set; }

        public decimal Fiyat { get; set; }
        public string Kategorisi { get; set; }
        public byte[] UrunResmi { get; set; }

        public static Urun[] Urunler()
        {
            Urun Aspirin = new Urun
            {
                Isim = "Aspirin",
                Fiyat = 2.99M
            };
            Urun Mazejik = new Urun
            {
                Isim = "Mazejik",
                Fiyat = 4.95M
            };
            Urun Krem = new Urun
            {
                Isim = "Nivea Krem",
                Fiyat = 4.95M
            };
            return new Urun[] { Aspirin, Mazejik, Krem };
        }
    }
}
