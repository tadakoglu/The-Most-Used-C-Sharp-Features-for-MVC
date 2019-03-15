using AlternetSiparisYazilimi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GirisProjesi2.Models
{
    public class Sepet
    {
        public IList<Urun> SepetUrunleri { get; set; }
        public Sepet()
        {

        }
    }
}
