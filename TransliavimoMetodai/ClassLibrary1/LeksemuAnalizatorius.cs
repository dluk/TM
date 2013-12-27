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
            bool rado = false;

            while (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != "{")
            {
                rado = false;
                foreach (var deklaravimoLeksema in DeklaravimoLeksemos)
                {
                    if (deklaravimoLeksema.Leksema.Contains(analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme))
                    {
                        rado = true;
                        deklaravimoLeksema.Analyze(analizatorius,
                            analizatorius.SintaksesMedis.Find(x => x.TevoId == TevoId).Id);

                        if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != ";")
                        {
                            throw new Exception("; expected " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme + " found");
                        }
                        analizatorius.Indeksas++;
                        break;
                    }
                }
                if (!rado)
                {
                    throw new Exception("incorrect declaration: " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme);
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
                throw new Exception("[ expected " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme + " found");
            }
            analizatorius.Indeksas++;
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Pavadinimas != "SveikasSkaicius")
            {
                throw new Exception("integer expected " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme + " found");
            }
            analizatorius.SintaksesMedis.Add(new Objektas("MasyvoDydis", analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme, masyvas.Id));
            analizatorius.Indeksas++;
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != "]")
            {
                throw new Exception("] expected " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme + " found");
            }
            analizatorius.Indeksas++;
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Pavadinimas != "identifikatorius")
            {
                throw new Exception("identifikatorius expected " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme + " found");
            }
            analizatorius.SintaksesMedis.Add(new Objektas("MasyvoIdentifikatorius", analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme, masyvas.Id));
            masyvas.Reiksme = analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme;
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
            TevoId = tevoId;
            var funkcija = new Objektas("FunkcijosDeklaravimas", "", TevoId);
            analizatorius.SintaksesMedis.Add(funkcija);
            analizatorius.Indeksas++;
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Pavadinimas != "identifikatorius")
            {
                throw new Exception("identifikatorius expected " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme + " found");
            }
            analizatorius.SintaksesMedis.Add(new Objektas("FunkcijosIdentifikatorius", analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme, funkcija.Id));
            funkcija.Reiksme = analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme;
            analizatorius.Indeksas++;
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != "returns")
            {
                throw new Exception("returns expected " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme + " found");
            }
            analizatorius.Indeksas++;
            var tipasAnal = new TipasAnal().Analyze(analizatorius, funkcija.Id);
            analizatorius.Indeksas++;
            var paramAnal = new ParametruAnal().Analyze(analizatorius, funkcija.Id);
            analizatorius.Indeksas++;
            
            new BlokasAnalizatorius().Analyze(analizatorius, funkcija.Id);
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
                throw new Exception("identifikatorius expected " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme + " found");
            }
            analizatorius.SintaksesMedis.Add(new Objektas("KintamojoIdentifikatorius", analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme, kintamasis.Id));
            kintamasis.Reiksme = analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme;
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
                throw new Exception("tipas expected " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme +
                                        " found");
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

    public class ParametruAnal : ILeksemuAnalizatorius
    {
        public List<string> Leksema { get; set; }
        public Guid TevoId { get; set; }

        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {
            TevoId = tevoId;
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != "(")
            {
                throw new Exception("( expected " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme + " found");
            }
            analizatorius.Indeksas++;
            while (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != ")")
            {
                var parametras = new Objektas("ParametroDeklaravimas", "", tevoId);
                analizatorius.SintaksesMedis.Add(parametras);
                var tipasAnal = new TipasAnal().Analyze(analizatorius, parametras.Id);
                analizatorius.Indeksas++;

                if (analizatorius.VarduLentele[analizatorius.Indeksas].Pavadinimas != "identifikatorius")
                {
                    throw new Exception("identifikatorius expected " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme + " found");
                }
                analizatorius.SintaksesMedis.Add(new Objektas("ParametroIdentifikatorius", analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme, parametras.Id));
                parametras.Reiksme = analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme;
                analizatorius.Indeksas++;
                if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != ")")
                {
                    if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme == ",")
                    {
                        analizatorius.Indeksas++;
                    }
                    else
                    {
                        throw new Exception(", expected " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme +
                                            " found");
                    }
                }
            }

            return "";
        }
   
    }
}
