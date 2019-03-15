using AlternetSiparisYazilimi.Models;
using GirisProjesi2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GirisProjesi2.Altyapi
{
    public static class ExtensionMetotlarim
    {
        /// <summary>
        /// Bu genişleme metodu Sepet yerine interface'lere de uygulanabilir, bu durumda interface'yi uygulayan tüm sınıflar bu genişleme sınıfı metotlarından faydalanabilecektir.
        /// </summary>
        /// <param name="sepet"></param>
        /// <returns></returns>
        public static int ToplamUrunSayisi(this Sepet sepet)
        {
            int toplam = sepet.SepetUrunleri.Count();            
            return toplam;
        }
        public static decimal ToplamFiyatiHesapla(this Sepet sepet)
        {
            decimal toplam = 0;
            foreach (Urun u in sepet.SepetUrunleri)
            {
                toplam += u?.Fiyat ?? 0; // u null değilse fiyatı ata ve topla, herhangi biri null ise 0 olarak ata ve topla.
            }
            return toplam;
        }
        public static IEnumerable<Urun> UcuzOlanlariGetir(this Sepet sepet)
        {
            foreach (Urun u in sepet.SepetUrunleri)
            {
                if ((u?.Fiyat ?? 0) < 20)
                {
                    yield return u; // yield return u'ların biriktirilerek döndürülmesini sağlar. IEnumerable<Urun> bir dizi döndürmüş oluruz.
                }
            }
        }
        public static IEnumerable<Urun> FiltreleDahaUcuz(this Sepet sepet, decimal deger)
        {
            foreach (Urun u in sepet.SepetUrunleri)
            {
                if ((u?.Fiyat ?? 0) < deger)
                {
                    yield return u; // yield return u'ların biriktirilerek döndürülmesini sağlar. IEnumerable<Urun> bir dizi döndürmüş oluruz.
                }
            }
        }
        public static IEnumerable<Urun> FiltreleDahaPahali(this Sepet sepet, decimal deger)
        {
            foreach (Urun u in sepet.SepetUrunleri)
            {
                if ((u?.Fiyat ?? 0) > deger)
                {
                    yield return u; // yield return u'ların biriktirilerek döndürülmesini sağlar. IEnumerable<Urun> bir dizi döndürmüş oluruz.
                }
            }
        }
        public static IEnumerable<Urun> FiltreleLambdali(this Sepet sepet, Func<Urun,bool> kıstasFonksiyonu)
        {
            foreach (Urun u in sepet.SepetUrunleri)
            {
                if (kıstasFonksiyonu(u)) // kıstas fonksiyonu göndereceğimiz parametre fonksiyona göre kendisi burayı ayarlayacaktır. bu fonksiyonları lambda ifadeleri ile delegate fonksiyon kullanmadan(kısa bir şekilde) yapacağız.
                {
                    yield return u; // yield return u'ların biriktirilerek döndürülmesini sağlar. IEnumerable<Urun> bir dizi döndürmüş oluruz.
                }
            }
        }

    }
}
