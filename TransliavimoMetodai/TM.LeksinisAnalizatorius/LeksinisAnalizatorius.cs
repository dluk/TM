using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.LeksinisAnalizatorius
{
    public class LeksinisAnalizatorius
    {
        public List<ILeksema> Leksemos = new List<ILeksema>()
        {
            new Kabutes(),
            new Skaicius(),
            new PavadinimasLeksema()
        };

        public List<ILeksema> SkiriamosiosLeksemos = new List<ILeksema>()
        {
            new Komentaras(),
            new Pliusiukas(),
            new Minusiukas(),
            new Kabliataskis(),
            new KairysLauztinisSkliaustas(),
            new KairysSkliaustas(),
            new DesinysLauztinisSkliaustas(),
            new DesinysSkliaustas(),
            new Daugyba(),
            new Daugiau(),
            new Lygu(),
            new Maziau(),
            new Tarpas(),
            new NaujaEilute(),
            new Kablelis(),
            new KairysRiestinisSkliaustas(),
            new DesinysRiestinisSkliaustas(),
            new Konkatenacija(),
            new Nelygu()
        }; 

        public string Programa;
        public List<LentelesLeksema> VarduLentele = new List<LentelesLeksema>();

        public char Simbolis;
        public string Zodis = "";
        public int Index = 0;
        
        public LeksinisAnalizatorius(string programa)
        {
            Programa = programa;
        }

        public bool Analizuoti()
        {
            
            while (Index<Programa.Count())
            {
                Simbolis = Programa[Index];
                var rado = false;
                foreach (ILeksema leksema in SkiriamosiosLeksemos.Union(Leksemos))
                {
                    if (leksema.Tinka(Simbolis))
                    {
                        rado = true;
                        var analize = leksema.Analize(this);
                        if(analize != null)
                            VarduLentele.Add(analize);
                        Zodis = "";
                        break;
                    }
                      
                }
                if(!rado)
                    VarduLentele.Add(new LentelesLeksema("klaida", Simbolis.ToString()));
                Index++;
            }
            
            return true;
        }

        #region helper metodai
        public override string ToString()
        {
            string rez = "";
            foreach (LentelesLeksema leksema in VarduLentele)
            {
                rez += string.Format("{{ {0}, {1} }}\r\n", leksema.Pavadinimas, leksema.Reiksme);
            }
            return rez;
        }

        public bool Peek()
        {
            return (Index + 1 < Programa.Count());
        }

        public char NextChar()
        {
            return (Programa[Index + 1]);

        }
        #endregion

    }
}
