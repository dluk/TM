using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.SintaksinisAnalizatorius
{
    public class Objektas
    {
        

        public Guid Id { get; set; }
        public string Tipas { get; set; }
        public string Reiksme { get; set; }
        public Guid TevoId { get; set; }

        public Objektas(string tipas, string reiksme, Guid tevoId)
        {
            Id = Guid.NewGuid();
            Tipas = tipas;
            Reiksme = reiksme;
            TevoId = tevoId;
        }
    }
}
