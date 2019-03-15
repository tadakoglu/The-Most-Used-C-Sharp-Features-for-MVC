using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GirisProjesi2.Models
{
    public class Async_Await_Kullanimi
    {
        //Asenkron istek yapmak için async ve await kullanacağım. Öncelikle eski yöntemle bir metot yazalım...
        public static Task<long?> SayfaBoyutuHesapla()
        {
            HttpClient musteri = new HttpClient();
            Task<HttpResponseMessage> asenkronGorev = musteri.GetAsync("http://pau.edu.tr");

            //asenksonGorev tamamlandığında ContinueWith ile yapılacak işlem lambda ifadesi return değeri ile belirtilir.
            Task<long?> byteOlarakBoyut = asenkronGorev.ContinueWith((Task<HttpResponseMessage> asenkronGorevKopyasi) => 
            //asenkron görev bittiğinde(get isteği) bu lambda ifadesi fonksiyonu çalışır. 
            {
                return asenkronGorevKopyasi.Result.Content.Headers.ContentLength; //the size of the entity-body, in bytes, sent to the recipient., long? olarak döner.
            });

            return byteOlarakBoyut; // sayfa boyutu byte cinsinden döndürülür.
        }

        // ÖNEMLİ 1: await kullanıldığında async eklenmek zorunda
        // ÖNEMLİ 2: await veya async anahtar kelimelerini kullanan bir metot mutlaka Task<type> döndürmeli ya da void olmalı.
        public static async Task<long?> SayfaBoyutuHesaplaYeniCSharp() 
        {
            HttpClient musteri = new HttpClient();
            HttpResponseMessage asenkronGorev = await musteri.GetAsync("http://pau.edu.tr"); //  getasync işini bitiresiye kadar bekle(await)
            //Bu satıra geçildiğinde GetAsync görevini artık tamamlamış demektir.
            long? byteOlarakBoyut = asenkronGorev.Content.Headers.ContentLength; //the size of the entity-body, in bytes, sent to the recipient., long? olarak döner.
            return byteOlarakBoyut; // sayfa boyutu byte cinsinden döndürülür.
        }
    }
}
