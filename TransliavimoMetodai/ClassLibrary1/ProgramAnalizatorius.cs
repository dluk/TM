using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.LeksinisAnalizatorius;

namespace TM.SintaksinisAnalizatorius
{
    internal class ProgramAnalizatorius
    {
        public List<ILeksemuAnalizatorius> ProgramosLeksemos = new List<ILeksemuAnalizatorius>()
        {
            new ReiksmeAnalizatorius()
        };


        public List<string> Leksema { get; set; }
        public Guid TevoId { get; set; }

        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {
            TevoId = tevoId;
            analizatorius.SintaksesMedis.Add(new Objektas("ProgramosBlokas", "", TevoId));

            while (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != "end")
            {
                foreach (var programosLeksema in ProgramosLeksemos)
                {
                    if (programosLeksema.Leksema.Contains(analizatorius.VarduLentele[analizatorius.Indeksas].Pavadinimas))
                    {
                        programosLeksema.Analyze(analizatorius,
                            analizatorius.SintaksesMedis.Find(x => x.TevoId == TevoId).Id);
                        break;
                    }
                }
            }


            return "";
        }
    }

    internal class WhileAnalizatorius : ILeksemuAnalizatorius
    {
        public List<string> Leksema
        {
            get { return new List<string>() {"while"}; }
            set { }
        }

        public Guid TevoId { get; set; }

        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {
            TevoId = tevoId;
            var whileBlokas = new Objektas("WhileBlokas", "", tevoId);
            analizatorius.SintaksesMedis.Add(whileBlokas);
            analizatorius.Indeksas++;
            return "";
        }
    }

    internal class LoginisAnalizatorius : ILeksemuAnalizatorius
    {
        public List<string> Leksema
        {
            get { return new List<string>() {"Daugiau", "Maziau", "DaugiauLygu", "MaziauLygu"}; }
            set { }
        }

        public Guid TevoId { get; set; }

        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {
            return "";
        }

    }

    internal class IsraiskaAnalizatorius : ILeksemuAnalizatorius
    {
        public List<string> Leksema
        {
            get { return new List<string>() {"Pliusas", "Minusas", "Daugyba", "Dalyba"}; }
            set { }
        }

        public Guid TevoId { get; set; }

        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {

            return "";
        }
    }

    internal class IndentifikatoriusAnalizatorius : ILeksemuAnalizatorius
    {
        public List<string> Leksema
        {
            get;
            set;
        }

        public Guid TevoId { get; set; }

        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {
            TevoId = tevoId;
            var identifikatorius = new Objektas("Identifikatorius", "", TevoId);
            analizatorius.SintaksesMedis.Add(identifikatorius);
            analizatorius.Indeksas++;
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme == "[")
            {
                analizatorius.SintaksesMedis.Add(new Objektas("IdentifikatoriausTipas", "Masyvas", identifikatorius.Id));
                analizatorius.Indeksas++;
                if (analizatorius.VarduLentele[analizatorius.Indeksas].Pavadinimas == "SveikasSkaicius")
                {
                    analizatorius.SintaksesMedis.Add(new Objektas("IeskomasElem",
                        analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme, identifikatorius.Id));
                    analizatorius.Indeksas++;
                }
                else if (analizatorius.VarduLentele[analizatorius.Indeksas].Pavadinimas == "Identifikatorius")
                {
                    new IndentifikatoriusAnalizatorius().Analyze(analizatorius, identifikatorius.Id);
                }
                else
                {
                    throw new Exception("Array reference expected," + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme + " found");
                }
                analizatorius.Indeksas++;
                if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != "]")
                {
                    throw new Exception("] expected," + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme + " found");
                }
            }
            else if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme == "(")
            {   
                analizatorius.SintaksesMedis.Add(new Objektas("IdentifikatoriausTipas", "Funkcija", identifikatorius.Id));
                analizatorius.Indeksas++;
                while (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != ")")
                {
                    var parametras = new Objektas("Parametras", "", identifikatorius.Id);
                    analizatorius.SintaksesMedis.Add(parametras);
                    if (analizatorius.VarduLentele[analizatorius.Indeksas].Pavadinimas == "identifikatorius")
                    {
                        new IndentifikatoriusAnalizatorius().Analyze(analizatorius, identifikatorius.Id);
                    }
                    else if (analizatorius.VarduLentele[analizatorius.Indeksas].Pavadinimas == "")
                    {
                        
                    }
                    
                }
            }
            return "";
        }
    }

    public class ReiksmeAnalizatorius : ILeksemuAnalizatorius
    {
        public List<string> Leksema
        {
            get { return new List<string>() { "SveikasSkaicius", "DesimtainisSkaicius", "Identifikatorius"}; }
            set { }
        }

        public Guid TevoId { get; set; }
        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {
           TevoId = tevoId;
           var reiksme = new Objektas("reiksme", "", TevoId);
           analizatorius.SintaksesMedis.Add(reiksme);
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Pavadinimas == "SveikasSkaicius")
            {
                analizatorius.SintaksesMedis.Add(new Objektas(
                    "SveikasSkaicius", analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme, reiksme.Id));
            }
            else if (analizatorius.VarduLentele[analizatorius.Indeksas].Pavadinimas == "DesimtainisSkaicius")
            {
                analizatorius.SintaksesMedis.Add(new Objektas(
                    "DesimtainisSkaicius", analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme, reiksme.Id));
            }
            else if (analizatorius.VarduLentele[analizatorius.Indeksas].Pavadinimas == "Identifikatorius")
            {
                new IndentifikatoriusAnalizatorius().Analyze(analizatorius, reiksme.Id);
            }
            analizatorius.Indeksas++;
            return "";
        }
    }

}
