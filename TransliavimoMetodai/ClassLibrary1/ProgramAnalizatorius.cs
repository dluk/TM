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
            new IsraiskaAnalizatorius()
        };


        public List<string> Leksema { get; set; }
        public Guid TevoId { get; set; }

        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {
            var programosBlokas = new Objektas("ProgramosBlokas", "", TevoId);
            analizatorius.SintaksesMedis.Add(programosBlokas);

            while (analizatorius.Indeksas < analizatorius.VarduLentele.Count && analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != "end")
            {
                foreach (var programosLeksema in ProgramosLeksemos)
                {
                    if (programosLeksema.Leksema.Contains(analizatorius.VarduLentele[analizatorius.Indeksas].Pavadinimas))
                    {
                        programosLeksema.Analyze(analizatorius,
                            programosBlokas.Id);
                        break;
                    }
                    else
                    {
                        new IsraiskaAnalizatorius().Analyze(analizatorius, programosBlokas.Id);
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
            get { return new List<string>() { "while" }; }
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
            get { return new List<string>() { "Daugiau", "Maziau", "DaugiauLygu", "MaziauLygu" }; }
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
            var identifikatorius = new Objektas("Identifikatorius", analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme, TevoId);
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

                }
                else if (analizatorius.VarduLentele[analizatorius.Indeksas].Pavadinimas == "identifikatorius")
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
                analizatorius.Indeksas++;
            }
            else if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme == "(")
            {
                analizatorius.SintaksesMedis.Add(new Objektas("IdentifikatoriausTipas", "Funkcija", identifikatorius.Id));
                analizatorius.Indeksas++;
                while (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != ")")
                {
                    
                    var parametras = new Objektas("Parametras", "", identifikatorius.Id);
                    analizatorius.SintaksesMedis.Add(parametras);
                    new ReiksmeAnalizatorius().Analyze(analizatorius, parametras.Id);

                    if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme == ",")
                        analizatorius.Indeksas++;
                    
                }
                analizatorius.Indeksas++;
            }
            return "";
        }
    }

    public class ReiksmeAnalizatorius : ILeksemuAnalizatorius
    {
        public List<string> Leksema
        {
            get { return new List<string>() { "SveikasSkaicius", "DesimtainisSkaicius", "identifikatorius" }; }
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
            else if (analizatorius.VarduLentele[analizatorius.Indeksas].Pavadinimas == "identifikatorius")
            {
                new IndentifikatoriusAnalizatorius().Analyze(analizatorius, reiksme.Id);
                analizatorius.Indeksas--;
            }
            analizatorius.Indeksas++;
            return "";
        }
    }

    public class IsraiskaEiluteAnalizatorius : ILeksemuAnalizatorius
    {
        public List<string> Leksema
        {
            get;
            set;
        }

        public Guid TevoId { get; set; }
        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {
            return "";
        }
    }

    public class DaugiklisAnalizatorius : ILeksemuAnalizatorius
    {
        public List<string> Leksema
        {
            get;
            set;
        }

        public Guid TevoId { get; set; }
        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {
            var daugiklis = new Objektas("Daugiklis", "", tevoId);
            analizatorius.SintaksesMedis.Add(daugiklis);
            new ReiksmeAnalizatorius().Analyze(analizatorius, daugiklis.Id);
            
            return "";
        }
    }

    internal class IsraiskaAnalizatorius : ILeksemuAnalizatorius
    {
        public List<string> Leksema
        {
            get { return new List<string>() { "Pliusas", "Minusas", "Daugyba", "Dalyba" }; }
            set { }
        }

        public Guid TevoId { get; set; }

        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {
            var israiska = new Objektas("Israiska", "", tevoId);
            analizatorius.SintaksesMedis.Add(israiska);

            while (true)
            {
                if (new[] {"+", "-"}.Contains(analizatorius.VarduLentele[analizatorius.Indeksas + 1].Reiksme))
                {
                    var israiska2 = new Objektas("Israiska", "", israiska.Id);
                    analizatorius.SintaksesMedis.Add(israiska2);
                    new TermasAnalizatorius().Analyze(analizatorius, israiska2.Id);
                }
                else
                {
                    new TermasAnalizatorius().Analyze(analizatorius, israiska.Id);
                }
                if (new[] {"+", "-", "*", "/"}.Contains(analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme))
                {
                    analizatorius.SintaksesMedis.Add(new Objektas("Operatorius", analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme, israiska.Id));
                    analizatorius.Indeksas += 1;
                }
                else
                {
                    break;
                }
            }


            return "";
        }
    }

    public class TermasAnalizatorius : ILeksemuAnalizatorius
    {
        public List<string> Leksema
        {
            get;
            set;
        }

        public Guid TevoId { get; set; }
        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {
            var termas = new Objektas("Termas", "", tevoId);
            analizatorius.SintaksesMedis.Add(termas);
           
            
            if (new[] { "*", "/" }.Contains(analizatorius.VarduLentele[analizatorius.Indeksas + 1].Reiksme))
            {
                var termas2 = new Objektas("Termas", "", termas.Id);
                analizatorius.SintaksesMedis.Add(termas2);
                new DaugiklisAnalizatorius().Analyze(analizatorius, termas2.Id);
            }
            else
            {
                new DaugiklisAnalizatorius().Analyze(analizatorius, termas.Id);
            }
            return "";
        }
    }

}
