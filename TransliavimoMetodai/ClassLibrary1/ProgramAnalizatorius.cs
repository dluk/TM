using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace TM.SintaksinisAnalizatorius
{
    internal class ProgramAnalizatorius
    {
        public List<ILeksemuAnalizatorius> ProgramosLeksemos = new List<ILeksemuAnalizatorius>()
        {
            new SakinysAnalizatorius(),
            new BlokasAnalizatorius()
        };


        public List<string> Leksema { get; set; }
        public Guid TevoId { get; set; }

        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {
            var programosBlokas = new Objektas("ProgramosBlokas", "", tevoId);
            analizatorius.SintaksesMedis.Add(programosBlokas);

            new BlokasAnalizatorius().Analyze(analizatorius, programosBlokas.Id);


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
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != "(")
            {
                throw new SyntaxException("( expected " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme + " found");
            }
            analizatorius.Indeksas++;
            new LogininisAnalizatorius().Analyze(analizatorius, whileBlokas.Id);
            //analizatorius.Indeksas++;
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != ")")
            {
                throw new SyntaxException(") expected " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme + " found");
            }
            analizatorius.Indeksas++;
            new BlokasAnalizatorius().Analyze(analizatorius, whileBlokas.Id);
            return "";
        }
    }

    internal class IfAnalizatorius : ILeksemuAnalizatorius
    {
        public List<string> Leksema
        {
            get { return new List<string>() { "if" }; }
            set { }
        }

        public Guid TevoId { get; set; }

        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {
            TevoId = tevoId;
            var whileBlokas = new Objektas("IfBlokas", "", tevoId);
            analizatorius.SintaksesMedis.Add(whileBlokas);
            analizatorius.Indeksas++;
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != "(")
            {
                throw new SyntaxException("( expected " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme + " found");
            }
            analizatorius.Indeksas++;
            new LogininisAnalizatorius().Analyze(analizatorius, whileBlokas.Id);
            
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != ")")
            {
                throw new SyntaxException(") expected " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme + " found");
            }
            analizatorius.Indeksas++;
            new BlokasAnalizatorius().Analyze(analizatorius, whileBlokas.Id);
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
                new IsraiskaAnalizatorius().Analyze(analizatorius, identifikatorius.Id);
                
                if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != "]")
                {
                    throw new SyntaxException("] expected, " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme + " found");
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
            else if (analizatorius.VarduLentele[analizatorius.Indeksas].Pavadinimas == "konstanta")
            {
                var konstanta = new Objektas("konstanta", analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme,
                    reiksme.Id);
                analizatorius.SintaksesMedis.Add(konstanta);
            }
            else
            {
                throw new SyntaxException("incorrect reiksme: " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme);
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
                if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme == "(")
                {
                    new SkliaustuAnalizatorius().Analyze(analizatorius, israiska.Id);
                }
                else if (new[] { "+", "-" }.Contains(analizatorius.VarduLentele[analizatorius.Indeksas + 1].Reiksme))
                {

                    var israiska2 = new Objektas("Israiska", "", israiska.Id);
                    analizatorius.SintaksesMedis.Add(israiska2);
                    new TermasAnalizatorius().Analyze(analizatorius, israiska2.Id);

                }else
                {
                    new TermasAnalizatorius().Analyze(analizatorius, israiska.Id);
                }
                if (new[] { "+", "-", "*", "/", "&", "(" }.Contains(analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme))
                {
                    var operatorius = new Objektas("operatorius", analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme, israiska.Id);
                    analizatorius.SintaksesMedis.Add(operatorius);
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

    public class SakinysAnalizatorius : ILeksemuAnalizatorius
    {
        public List<string> Leksema
        {
            get { return new List<string> { "for", "while", "if" }; }
            set { }
        }
        public Guid TevoId { get; set; }

        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {
            var sakinys = new Objektas("Sakinys", "", tevoId);
            analizatorius.SintaksesMedis.Add(sakinys);
            bool deklaravimas = false;

            if (analizatorius.VarduLentele[analizatorius.Indeksas].Pavadinimas == "identifikatorius")
            {
                new PriskyrimoAnalizatorius().Analyze(analizatorius, sakinys.Id);
            }
            else if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme == "while")
            {
                new WhileAnalizatorius().Analyze(analizatorius, sakinys.Id);
            }
            else if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme == "for")
            {
                new ForAnalizatorius().Analyze(analizatorius, sakinys.Id);
            }
            else if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme == "if")
            {
                new IfAnalizatorius().Analyze(analizatorius, sakinys.Id);
            }
             else if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme == "read" || analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme == "print")
            {
                new IoAnalizatorius().Analyze(analizatorius, sakinys.Id);
            }

            else if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme == "return")
            {
                new ReturnAnalizatorius().Analyze(analizatorius, sakinys.Id);
            }
            else
            {
                deklaravimas = false;
                List<ILeksemuAnalizatorius> DeklaravimoLeksemos = new List<ILeksemuAnalizatorius>()
                {
                    new MasyvoDeklaravimoAnal(),
                    new KintamojoDeklaravimoAnal()
                };
                foreach (var deklaravimoLeksema in DeklaravimoLeksemos)
                {
                    if (deklaravimoLeksema.Leksema.Contains(analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme))
                    {
                        deklaravimoLeksema.Analyze(analizatorius, sakinys.Id);
                        deklaravimas = true;
                        break;
                    }
                }
                if (deklaravimas == false)
                {
                    throw new SyntaxException("unexpected " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme);
                }
            }
            
            
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != ";")
            {
                throw new SyntaxException("\";\" expected " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme + " found");
            }
            analizatorius.Indeksas++;

            return "";
        }
    }

    public class PriskyrimoAnalizatorius : ILeksemuAnalizatorius
    {
        public List<string> Leksema
        {
            get { return new List<string> { "identifikatorius" }; }
            set { }
        }
        public Guid TevoId { get; set; }

        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {
            var priskyrimas = new Objektas("Priskyrimas", "", tevoId);
            
            analizatorius.SintaksesMedis.Add(priskyrimas);
            new IndentifikatoriusAnalizatorius().Analyze(analizatorius, priskyrimas.Id);
            
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != "=")
            {
                throw new SyntaxException("\"=\" expected, but " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme + " found");
            }
            var operatorius = new Objektas("operatorius", "=", priskyrimas.Id);
            analizatorius.SintaksesMedis.Add(operatorius);
            analizatorius.Indeksas++;
            new IsraiskaAnalizatorius().Analyze(analizatorius, priskyrimas.Id);

            return "";
        }
    }

    public class BlokasAnalizatorius : ILeksemuAnalizatorius
    {
        public List<string> Leksema
        {
            get { return new List<string> { "{" }; }
            set { }
        }
        public Guid TevoId { get; set; }

        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {
            var blokas = new Objektas("Blokas", "", tevoId);
            analizatorius.SintaksesMedis.Add(blokas);
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != "{")
            {
                throw new SyntaxException("\"{\" expected " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme + " found");
            }
            analizatorius.Indeksas++;
            
            while (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != "}")
            {
                new SakinysAnalizatorius().Analyze(analizatorius, blokas.Id);
            }
            analizatorius.Indeksas++;
            return "";
            
        }

    }


    public class LogininisAnalizatorius : ILeksemuAnalizatorius
    {
        public List<string> Leksema
        {
            get { return new List<string>() { }; }
            set { }
        }
        public Guid TevoId { get; set; }

        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {
            var loginis = new Objektas("Loginis_reiskinys", "", tevoId);
            analizatorius.SintaksesMedis.Add(loginis);

            while (true)
            {
                new IsraiskaAnalizatorius().Analyze(analizatorius, loginis.Id);
                if (new[] { ">", "<", ">=", "<=", "!=", "=" }.Contains(analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme))
                {
                    analizatorius.SintaksesMedis.Add(new Objektas("Loginis_operatorius", analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme, loginis.Id));
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

    public class ForAnalizatorius : ILeksemuAnalizatorius
    {
        public List<string> Leksema
        {
            get { return new List<string>() {"for"}; }
            set { }
        }
        public Guid TevoId { get; set; }

        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {
            var foras = new Objektas("For_sakinys", "", tevoId);
            analizatorius.SintaksesMedis.Add(foras);
            analizatorius.Indeksas++;
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != "(")
            {
                throw new SyntaxException("( Expected " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme + " found");
            }
            analizatorius.Indeksas++;
            new PriskyrimoAnalizatorius().Analyze(analizatorius, foras.Id);
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != ",")
            {
                throw new SyntaxException(", Expected " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme + " found");
            }
            analizatorius.Indeksas++;
            new LogininisAnalizatorius().Analyze(analizatorius, foras.Id);
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != ",")
            {
                throw new SyntaxException(", Expected " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme + " found");
            }
            analizatorius.Indeksas++;
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Pavadinimas != "SveikasSkaicius")
            {
                throw new SyntaxException("SveikasSkaicius Expected " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme + " found");
            }
            analizatorius.Indeksas++;
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != ")")
            {
                throw new SyntaxException(") Expected " + analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme + " found");
            }
            analizatorius.Indeksas++;
            new BlokasAnalizatorius().Analyze(analizatorius, foras.Id);
            return "";
        }
    }

    public class IoAnalizatorius : ILeksemuAnalizatorius
    {
        public List<string> Leksema
        {
            get { return new List<string>() { "read","print" }; }
            set { }
        }
        public Guid TevoId { get; set; }

        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {
            var io = new Objektas("IO", "", tevoId);
            analizatorius.SintaksesMedis.Add(io);
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme == "print")
            {
                analizatorius.Indeksas++;
                var output = new Objektas("output", "", io.Id);
                new IsraiskaAnalizatorius().Analyze(analizatorius, output.Id);
            }
            else if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme == "read")
            {
                analizatorius.Indeksas++;
                var input = new Objektas("input", "", io.Id);
                new IndentifikatoriusAnalizatorius().Analyze(analizatorius, input.Id);
            }
            return "";
        }
    }

    public class ReturnAnalizatorius : ILeksemuAnalizatorius
    {
        public List<string> Leksema
        {
            get { return new List<string>() { "return"}; }
            set { }
        }
        public Guid TevoId { get; set; }

        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {
            var returns = new Objektas("return", "", tevoId);
            analizatorius.SintaksesMedis.Add(returns);
            analizatorius.Indeksas ++;
            new IsraiskaAnalizatorius().Analyze(analizatorius, returns.Id);
            return "";
        }
    }

    public class SkliaustuAnalizatorius : ILeksemuAnalizatorius
    {
        public List<string> Leksema
        {
            get { return new List<string>() {"("}; }
            set { }
        }

        public Guid TevoId { get; set; }

        public string Analyze(SintaksinisAnalizatorius analizatorius, Guid tevoId)
        {
            var termas = new Objektas("Termas", "", tevoId);
            analizatorius.SintaksesMedis.Add(termas);
            var daugiklis = new Objektas("Daugiklis", "", termas.Id);
            analizatorius.SintaksesMedis.Add(daugiklis);
            analizatorius.Indeksas++;
            if (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme == ")")
            {
                throw new SyntaxException("Israiska expected");
            }
            while (analizatorius.VarduLentele[analizatorius.Indeksas].Reiksme != ")")
            {
                new IsraiskaAnalizatorius().Analyze(analizatorius, daugiklis.Id);
            }
            analizatorius.Indeksas++;

            return "";
        }
    }
}
