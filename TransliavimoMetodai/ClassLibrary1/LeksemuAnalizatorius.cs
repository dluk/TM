using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TM.LeksinisAnalizatorius;

namespace TM.SintaksinisAnalizatorius
{
    public interface ILeksemuAnalizatorius
    {
        List<string> Leksema { get; set; }

        List<LentelesLeksema> VarduLentele { get; set; }

        string Analyze(int zodis);

    }

    public class LeksemuAnalizatorius : ILeksemuAnalizatorius
    {
        public List<string> Leksema { get; set; }

        public List<LentelesLeksema> VarduLentele { get; set; }

        public LeksemuAnalizatorius(List<LentelesLeksema> VarduLentele)
        {
            this.VarduLentele = VarduLentele;
        }

        public LeksemuAnalizatorius()
        {
        }

        public string Analyze(int zodis)
        {
            return "";
        }
    }

    public class DeklaravimoAnal : LeksemuAnalizatorius
    {

        public List<ILeksemuAnalizatorius> DeklaravimoLeksemos = new List<ILeksemuAnalizatorius>()
        {
            new MasyvoDeklaravimoAnal(),
            new FunkcijosDeklaravimoAnal(),
            new KintamojoDeklaravimoAnal()
        };

        public new string Analyze(int zodis)
        {
            while(VarduLentele[zodis].Reiksme != "begin")
            {
                foreach (var deklaravimoLeksema in DeklaravimoLeksemos)
                {
                    if (deklaravimoLeksema.Leksema.Contains(VarduLentele[zodis].Reiksme))
                    {
                        deklaravimoLeksema.Analyze(zodis);
                        break;
                    }
                }
            }
        }
    }

    public class MasyvoDeklaravimoAnal : LeksemuAnalizatorius
    {
        public MasyvoDeklaravimoAnal()
        {
            Leksema = new List<string>() {"array"};
        }

        public new string Analyze(int zodis)
        {
            return "";
        }

    }

    public class FunkcijosDeklaravimoAnal : LeksemuAnalizatorius
    {
        public FunkcijosDeklaravimoAnal()
        {
            Leksema = new List<string>() { "function" };
        }

        public new string Analyze(int zodis)
        {
            return "";
        }

    }

    public class KintamojoDeklaravimoAnal : LeksemuAnalizatorius
    {
        public KintamojoDeklaravimoAnal()
        {
            Leksema = new List<string>() {"string", "int", "dec"};
        }

        public new string Analyze(int zodis)
        {   
            TipasAnal tipasAnal = new TipasAnal();
            return tipasAnal.Analyze(zodis);
        }

    }

    public class TipasAnal : LeksemuAnalizatorius
    {
        public TipasAnal()
        {
            Leksema = new List<string>(){"string","int","dec"};
        }

        public new string Analyze(int zodis)
        {
            if (Leksema.IndexOf(VarduLentele[zodis].Reiksme) == 0)
            {
                return zodis + ":neegzistuojantis tipas";
            }
            else
            {
                return "";
            }
        }
    }
}
