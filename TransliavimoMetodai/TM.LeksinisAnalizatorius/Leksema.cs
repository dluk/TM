using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.LeksinisAnalizatorius
{
    public class LentelesLeksema
    {
        public string Pavadinimas { get; set; }
        public string Reiksme { get; set; }

        public LentelesLeksema(string pav, string reiksme)
        {
           
            Pavadinimas = pav;
            Reiksme = reiksme;
            
        }
    }
}
