using System;
using System.Collections.Generic;
using System.Linq;
using TM.LeksinisAnalizatorius;

namespace TM.SintaksinisAnalizatorius
{
    public interface ILeksemuAnalizatorius
    {
        List<string> Leksema { get; set; }
        Guid TevoId { get; set; }
        string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId);

    }

    //public class LeksemuAnalizatorius : ILeksemuAnalizatorius
    //{
    //    public List<string> Leksema { get; set; }

    //    public List<LentelesLeksema> VarduLentele { get; set; }

    //    public LeksemuAnalizatorius(List<LentelesLeksema> varduLentele)
    //    {
    //        this.VarduLentele = varduLentele;
    //    }

    //    public LeksemuAnalizatorius()
    //    {
    //    }

    //    public string Analyze(int zodis)
    //    {
    //        return "";
    //    }
    //}

    public class DeklaravimoAnal : ILeksemuAnalizatorius
    {

        public List<ILeksemuAnalizatorius> DeklaravimoLeksemos = new List<ILeksemuAnalizatorius>()
        {
            new MasyvoDeklaravimoAnal(),
            new FunkcijosDeklaravimoAnal(),
            new KintamojoDeklaravimoAnal()
        };
        

        public List<string> Leksema { get; set; }
        public Guid TevoId { get; set; }

        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {
            TevoId = tevoId;
            analizatorius.SintaksesMedis.Add(new Objektas("DeklaravimoBlokas", "", TevoId));
            
            while (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != "begin")
            {
                foreach (var deklaravimoLeksema in DeklaravimoLeksemos)
                {
                    if (deklaravimoLeksema.Leksema.Contains(analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme))
                    {
                        deklaravimoLeksema.Analyze(analizatorius, analizatorius.SintaksesMedis.Find(x=>x.TevoId == TevoId).Id);
                        break;
                    }
                }
            }
                
            
            return "";
        }
    }

    public class MasyvoDeklaravimoAnal : ILeksemuAnalizatorius
    {
        public List<string> Leksema {
            get { return new List<string>() {"array"}; } 
            set {}
        }

        public Guid TevoId { get; set; }

        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {
            TevoId = tevoId;
            var masyvas = new Objektas("MasyvoDeklaravimas", "", tevoId);
            analizatorius.SintaksesMedis.Add(masyvas);
            analizatorius.Indeksas++;
            var tipasAnal = new TipasAnal().Analyze(analizatorius, masyvas.Id);
            analizatorius.Indeksas++;
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != "[")
            {
                throw new Exception("[ expected");
            }
            analizatorius.Indeksas++;
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Pavadinimas != "SveikasSkaicius")
            {
                throw new Exception("integer expected");
            }
            analizatorius.SintaksesMedis.Add(new Objektas("MasyvoDydis", analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme, masyvas.Id));
            analizatorius.Indeksas++;
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != "]")
            {
                throw new Exception("] expected");
            }
            analizatorius.Indeksas++;
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Pavadinimas != "identifikatorius")
            {
                throw new Exception("identifikatorius expected");
            }
            analizatorius.SintaksesMedis.Add(new Objektas("MasyvoIdentifikatorius", analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme, masyvas.Id));
            analizatorius.Indeksas++;
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != ";")
            {
                throw new Exception("; expected");
            }
            analizatorius.Indeksas++;
            return "";

        }

    }

    public class FunkcijosDeklaravimoAnal : ILeksemuAnalizatorius
    {
        public List<string> Leksema {
            get { return new List<string>() {"function"}; } 
            set {}
        }

        public Guid TevoId { get; set; }

        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {
            return "";
        }

    }

    public class KintamojoDeklaravimoAnal : ILeksemuAnalizatorius
    {
         public List<string> Leksema {
            get { return new List<string>() {"string", "int", "dec"}; } 
            set {}
        }

        public Guid TevoId { get; set; }


        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {
            TevoId = tevoId;
            var kintamasis = new Objektas("KintamojoDeklaravimas", "", tevoId);
            analizatorius.SintaksesMedis.Add(kintamasis);
            var tipasAnal = new TipasAnal().Analyze(analizatorius, kintamasis.Id);
            analizatorius.Indeksas++;
            
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Pavadinimas != "identifikatorius")
            {
                throw new Exception("identifikatorius expected");
            }
            analizatorius.SintaksesMedis.Add(new Objektas("KintamojoIdentifikatorius", analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme, kintamasis.Id));
            analizatorius.Indeksas++;
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != ";")
            {
                throw new Exception("; expected");
            }
            analizatorius.Indeksas++;
            return "";
        }

    }

    public class TipasAnal : ILeksemuAnalizatorius
    {
        public List<string> Leksema
        {
            get { return new List<string>() { "string", "int", "dec" }; }
            set { }
        }

        public Guid TevoId { get; set; }

        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {
            var index = Leksema.IndexOf(analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme);
            if (index == -1)
            {
                throw new Exception("nera tokio tipo nx");
            }
            else
            {
                var reiksme = Leksema[index];
                var tipas = new Objektas("Tipas", reiksme, tevoId);
                analizatorius.SintaksesMedis.Add(tipas);
            }

            return "";
            
        }
    }
}
