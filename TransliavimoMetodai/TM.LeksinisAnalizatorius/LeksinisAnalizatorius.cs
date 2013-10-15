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
            new Maziau()
        }; 

        public string Programa;
        public List<LentelesLeksema> VarduLentele = new List<LentelesLeksema>();

        public char Simbolis;
        public string Zodis = "";
        private bool kabutese = false;
        public StringReader Reader;
        public int Index = 0;
        
        public LeksinisAnalizatorius(string programa, List<ILeksema> leksemos, int indeksas=0)
        {
            Programa = programa;//.Replace("\r", " ").Replace("\n", " ");
            if (leksemos != null)
                Leksemos = leksemos;
            Index = indeksas;
        }

        public bool Analizuoti()
        {
            Reader = new StringReader(Programa);
            using (Reader)
            {
                while (Index<Programa.Count())
                {
                    Simbolis = Programa[Index];
                    foreach (ILeksema leksema in SkiriamosiosLeksemos.Union(Leksemos))
                    {
                        if (leksema.Tinka(Simbolis))
                        {
                            var analize = leksema.Analize(this);
                            if(analize != null)
                                VarduLentele.Add(analize);
                            else VarduLentele.Add(new LentelesLeksema("klaida", Zodis));
                            Zodis = "";
                            break;

                        }
                           
                    }
                    Index++;
                }
                //pridetiILentele(_zodis);
            }
            
            return true;
        }

        public override string ToString()
        {
            string rez = "";
            foreach (LentelesLeksema leksema in VarduLentele)
            {
                rez += leksema.Pavadinimas + ", " + leksema.Reiksme + "\r\n";
            }
            return rez;
        }

        //private void pridetiILentele(string reiksme)
        //{
        //    if (!string.IsNullOrEmpty(reiksme))
        //    {
        //        switch (reiksme)
        //        {
        //            case ";":
        //                VarduLentele.Add(new LentelesLeksema("kabliataškis", ";"));
        //        }
        //        VarduLentele.Add(new LentelesLeksema(pav, reiksme));
        //        Zodis = "";
        //    }
        //}

    }
}
