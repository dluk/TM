using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TM.LeksinisAnalizatorius
{
    public interface ILeksema
    {
        string Pavadinimas { get; }
        bool Tinka(char charas);
        LentelesLeksema Analize(LeksinisAnalizatorius analizatorius);
    }

    #region operatoriai
    public class Pliusiukas : ILeksema
    {
        public string Pavadinimas { get { return "pliusiukas"; } }

        public bool Tinka(char charas)
        {
            return charas == '+';
        }
        public LentelesLeksema Analize(LeksinisAnalizatorius analizatorius)
        {
            return new LentelesLeksema(Pavadinimas, "+");
        }
    }

    public class Daugiau : ILeksema
    {
        public string Pavadinimas { get { return "Daugiau"; } }

        public bool Tinka(char charas)
        {
            return charas == '>';
        }
        public LentelesLeksema Analize(LeksinisAnalizatorius analizatorius)
        {
            return new LentelesLeksema(Pavadinimas, ">");
        }
    }

    public class Maziau : ILeksema
    {
        public string Pavadinimas { get { return "Maziau"; } }

        public bool Tinka(char charas)
        {
            return charas == '<';
        }
        public LentelesLeksema Analize(LeksinisAnalizatorius analizatorius)
        {
            return new LentelesLeksema(Pavadinimas, "<");
        }
    }

    public class Lygu : ILeksema
    {
        public string Pavadinimas { get { return "Lygu"; } }

        public bool Tinka(char charas)
        {
            return charas == '=';
        }
        public LentelesLeksema Analize(LeksinisAnalizatorius analizatorius)
        {
            return new LentelesLeksema(Pavadinimas, "=");
        }
    }

    public class Minusiukas : ILeksema
    {
        public string Pavadinimas { get { return "Minusiukas"; } }

        public bool Tinka(char charas)
        {
            return charas == '-';
        }
        public LentelesLeksema Analize(LeksinisAnalizatorius analizatorius)
        {
            return new LentelesLeksema(Pavadinimas, "-");
        }
    }
    public class Daugyba : ILeksema
    {
        public string Pavadinimas { get { return "Daugyba"; } }

        public bool Tinka(char charas)
        {
            return charas == '*';
        }
        public LentelesLeksema Analize(LeksinisAnalizatorius analizatorius)
        {
            return new LentelesLeksema(Pavadinimas, "*");
        }
    }
    #endregion

    #region skliaustai
    public class KairysSkliaustas : ILeksema
    {
        public string Pavadinimas { get { return "KairysSkliaustas"; } }

        public bool Tinka(char charas)
        {
            return charas == '(';
        }
        public LentelesLeksema Analize(LeksinisAnalizatorius analizatorius)
        {
            return new LentelesLeksema(Pavadinimas, "(");
        }
    }

    public class DesinysSkliaustas : ILeksema
    {
        public string Pavadinimas { get { return "DesinysSkliaustas"; } }

        public bool Tinka(char charas)
        {
            return charas == ')';
        }
        public LentelesLeksema Analize(LeksinisAnalizatorius analizatorius)
        {
            return new LentelesLeksema(Pavadinimas, ")");
        }
    }

    public class Kabliataskis : ILeksema
    {
        public string Pavadinimas { get { return "Kabliataskis"; } }

        public bool Tinka(char charas)
        {
            return charas == ';';
        }
        public LentelesLeksema Analize(LeksinisAnalizatorius analizatorius)
        {
            return new LentelesLeksema(Pavadinimas, ";");
        }
    }

    public class KairysLauztinisSkliaustas : ILeksema
    {
        public string Pavadinimas { get { return "KairysLauztinisSkliaustas"; } }

        public bool Tinka(char charas)
        {
            return charas == '[';
        }
        public LentelesLeksema Analize(LeksinisAnalizatorius analizatorius)
        {
            return new LentelesLeksema(Pavadinimas, "[");
        }
    }

    public class DesinysLauztinisSkliaustas : ILeksema
    {
        public string Pavadinimas { get { return "DesinysLauztinisSkliaustas"; } }

        public bool Tinka(char charas)
        {
            return charas == ']';
        }
        public LentelesLeksema Analize(LeksinisAnalizatorius analizatorius)
        {
            return new LentelesLeksema(Pavadinimas, "]");
        }
    }
    #endregion

    #region sisteminiai zodziai 

    #endregion

    public class PavadinimasLeksema : ILeksema
    {
        public string Pavadinimas { get { return "PavadinimasLeksema"; } }

        public bool Tinka(char charas)
        {
            return Regex.IsMatch(charas.ToString(), "[a-zA-Z]");
        }
        public LentelesLeksema Analize(LeksinisAnalizatorius analizatorius)
        {
            bool error = false;
            bool rado = false;
            //analizatorius.Zodis += analizatorius.Simbolis;
            while (analizatorius.Index < analizatorius.Programa.Count() )
            {
                analizatorius.Simbolis = analizatorius.Programa[analizatorius.Index];
                if (Regex.IsMatch(analizatorius.Simbolis.ToString(), "[a-zA-Z0-9]"))
                {
                    analizatorius.Zodis += analizatorius.Simbolis;
                }
                else
                {
                    error = true;
                    break;
                }
                if (analizatorius.Index + 1 < analizatorius.Programa.Count() &&
                    (analizatorius.SkiriamosiosLeksemos.Any(x => x.Tinka(analizatorius.Programa[analizatorius.Index + 1])) ||
                    analizatorius.Programa[analizatorius.Index + 1] == ' ' || analizatorius.Programa[analizatorius.Index + 1] == '\n'))
                {
                    rado = true;
                    break;
                }
                
                analizatorius.Index++;

            }
            if (analizatorius.Index == analizatorius.Programa.Count())
                rado = true;

            if (!rado) error = true;
            return error ? null : new LentelesLeksema("pavadinimasLeksema", analizatorius.Zodis);
            
        }
    }

    public class Skaicius : ILeksema
    {
        public string Pavadinimas { get { return "skaicius"; } }
        public bool Tinka(char charas)
        {
            return (Regex.IsMatch(charas.ToString(), "[0-9]"));
        }
        public LentelesLeksema Analize(LeksinisAnalizatorius analizatorius)
        {
            bool error = false;
            bool rado = false;
            //analizatorius.Zodis += analizatorius.Simbolis;
            while (analizatorius.Index < analizatorius.Programa.Count())
            {
                analizatorius.Simbolis = analizatorius.Programa[analizatorius.Index];
                int sk;
                if (int.TryParse(analizatorius.Simbolis.ToString(), out sk))
                {
                    analizatorius.Zodis += analizatorius.Simbolis;
                }
                else
                {
                    error = true;
                    break;
                }
                if (analizatorius.Index + 1 < analizatorius.Programa.Count() &&
                    (analizatorius.SkiriamosiosLeksemos.Any(
                        x => x.Tinka(analizatorius.Programa[analizatorius.Index + 1])) ||
                     analizatorius.Programa[analizatorius.Index + 1] == ' ' ||
                     analizatorius.Programa[analizatorius.Index + 1] == '\n'))
                {
                    rado = true;
                    break;
                }
                    
                analizatorius.Index++;
                
            }
            if (analizatorius.Index == analizatorius.Programa.Count())
                rado = true;

            if (!rado) error = true;
            return error ? null : new LentelesLeksema("skaicius", analizatorius.Zodis);
        }
    }

    public class Kabutes : ILeksema
    {
        public string Pavadinimas { get { return "Konstanta"; } }
        public bool Tinka(char charas)
        {
            return charas == '\"';
        }

        public LentelesLeksema Analize(LeksinisAnalizatorius analizatorius)
        {
            bool error = false;
            bool rado = false;
            //analizatorius.Zodis += analizatorius.Simbolis;
            analizatorius.Index++;
            while (analizatorius.Index < analizatorius.Programa.Count())
            {
                analizatorius.Simbolis = analizatorius.Programa[analizatorius.Index];
                if (Regex.IsMatch(analizatorius.Simbolis.ToString(), "[a-zA-Z0-9]"))
                {
                    analizatorius.Zodis += analizatorius.Simbolis;
                }
                else
                {
                    analizatorius.Zodis += analizatorius.Simbolis;
                    error = true;
                }
                if (analizatorius.Index + 1 < analizatorius.Programa.Count() &&
                     analizatorius.Programa[analizatorius.Index + 1] == '\"')
                {
                    rado = true;
                    analizatorius.Index++;
                    break;
                }

                analizatorius.Index++;
            }
            
            if (!rado) error = true;
            return error? null : new LentelesLeksema("konstanta", analizatorius.Zodis);
        }
    }

}
