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

    public static class SystemKeywords
    {
        public static List<string> Vardai = new List<string>() { "array", "string", "int", "return", "begin", "end", "function", "and", "or", "while", "if", "else", "forward", "for", "print", "read" };
    }

    #region skiriamieji
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
            if (analizatorius.Peek() && analizatorius.NextChar() == '=')
            {
                analizatorius.Index++;
                return new LentelesLeksema("DaugiauLygu", ">=");
            }
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
            if (analizatorius.Peek() && analizatorius.NextChar() == '=')
            {
                analizatorius.Index++;
                return new LentelesLeksema("DaugiauLygu", "<=");
            }
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
            if (analizatorius.Peek() && new Skaicius().Tinka(analizatorius.NextChar()))
            {
                analizatorius.Zodis = "-";
                analizatorius.Index++;
                return new Skaicius().Analize(analizatorius);
            }
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
    public class Kablelis : ILeksema
    {
        public string Pavadinimas { get { return "Kablelis"; } }

        public bool Tinka(char charas)
        {
            return charas == ',';
        }
        public LentelesLeksema Analize(LeksinisAnalizatorius analizatorius)
        {
            return new LentelesLeksema(Pavadinimas, ",");
        }
    }
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
    public class KairysRiestinisSkliaustas : ILeksema
    {
        public string Pavadinimas { get { return "KairysRiestinisSkliaustas"; } }

        public bool Tinka(char charas)
        {
            return charas == '{';
        }
        public LentelesLeksema Analize(LeksinisAnalizatorius analizatorius)
        {
            return new LentelesLeksema(Pavadinimas, "{");
        }
    }
    public class DesinysRiestinisSkliaustas : ILeksema
    {
        public string Pavadinimas { get { return "DesinysRiestinisSkliaustas"; } }

        public bool Tinka(char charas)
        {
            return charas == '}';
        }
        public LentelesLeksema Analize(LeksinisAnalizatorius analizatorius)
        {
            return new LentelesLeksema(Pavadinimas, "}");
        }
    }
    public class Konkatenacija : ILeksema
    {
        public string Pavadinimas { get { return "Konkatenacija"; } }

        public bool Tinka(char charas)
        {
            return charas == '&';
        }
        public LentelesLeksema Analize(LeksinisAnalizatorius analizatorius)
        {
            return new LentelesLeksema(Pavadinimas, "&");
        }
    }
    public class Nelygu : ILeksema
    {
        public string Pavadinimas { get { return "Nelygu"; } }

        public bool Tinka(char charas)
        {
            return charas == '!';
        }
        public LentelesLeksema Analize(LeksinisAnalizatorius analizatorius)
        {
            if (analizatorius.Peek() && analizatorius.NextChar() == '=')
            {
                analizatorius.Index++;
                return new LentelesLeksema(Pavadinimas, "!=");
            }
            else
            {
                return  new LentelesLeksema("klaida", "!");
            }
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
    public class Tarpas : ILeksema
    {
        public string Pavadinimas { get { return " "; } }

        public bool Tinka(char charas)
        {
            return charas == ' ';
        }
        public LentelesLeksema Analize(LeksinisAnalizatorius analizatorius)
        {
            return null;
        }
    }
    public class NaujaEilute : ILeksema
    {
        public string Pavadinimas { get { return " "; } }

        public bool Tinka(char charas)
        {
            return charas == '\n';
        }
        public LentelesLeksema Analize(LeksinisAnalizatorius analizatorius)
        {
            return null;
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
    public class Komentaras : ILeksema
    {
        public string Pavadinimas { get { return ""; } }
        public bool Tinka(char charas)
        {
            return charas == '/';
        }

        public LentelesLeksema Analize(LeksinisAnalizatorius analizatorius)
        {
            bool rado = false;
            string zodis = analizatorius.Programa[analizatorius.Index].ToString();
            if (analizatorius.Peek() &&
                analizatorius.NextChar() == '*')
            {
                analizatorius.Index++;
                while (analizatorius.Index < analizatorius.Programa.Count())
                {
                    analizatorius.Simbolis = analizatorius.Programa[analizatorius.Index];
                    zodis += analizatorius.Simbolis;

                    if (analizatorius.Simbolis == '*')
                    {
                        if (analizatorius.Peek() &&
                            analizatorius.NextChar() == '/')
                        {
                            rado = true;
                            analizatorius.Index++;
                            zodis += analizatorius.Programa[analizatorius.Index];
                            break;
                        }
                    }
                    analizatorius.Index++;
                }
                if (!rado)
                    return new LentelesLeksema("klaida", zodis);
                return new LentelesLeksema("komentaras", zodis);
            }
            return new LentelesLeksema("klaida", zodis);

        }
    }
    #endregion

    #region leksemos kurios nelaikomos skirtuku
    public class PavadinimasLeksema : ILeksema
    {
        public string Pavadinimas { get { return "identifikatorius"; } }

        public bool Tinka(char charas)
        {
            return Regex.IsMatch(charas.ToString(), "[a-zA-Z]");
        }
        public LentelesLeksema Analize(LeksinisAnalizatorius analizatorius)
        {
            while (analizatorius.Index < analizatorius.Programa.Count() )
            {
                analizatorius.Simbolis = analizatorius.Programa[analizatorius.Index];
                if (Regex.IsMatch(analizatorius.Simbolis.ToString(), "[a-zA-Z0-9]"))
                {
                    analizatorius.Zodis += analizatorius.Simbolis;
                }
                else
                {
                    analizatorius.Index--;
                    break;
                }
                if (analizatorius.Peek() &&
                    (analizatorius.SkiriamosiosLeksemos.Any(x => x.Tinka(analizatorius.NextChar()))))
                {
                   
                    break;
                }
                
                analizatorius.Index++;

            }
           
            if(SystemKeywords.Vardai.Contains(analizatorius.Zodis))
                return new LentelesLeksema("sisteminisZodis", analizatorius.Zodis);
            return new LentelesLeksema(Pavadinimas, analizatorius.Zodis);
            
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
                    analizatorius.Zodis += analizatorius.Simbolis;
                 
                }
                if (analizatorius.Peek() && analizatorius.SkiriamosiosLeksemos.Any(x => x.Tinka(analizatorius.NextChar())))
                {
                    rado = true;
                    break;
                }
                    
                analizatorius.Index++;
                
            }
            if (analizatorius.Index == analizatorius.Programa.Count())
                rado = true;

            if (!rado) error = true;
            return error ? new LentelesLeksema("klaida", analizatorius.Zodis) : new LentelesLeksema(Pavadinimas, analizatorius.Zodis);
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
           
            analizatorius.Index++;
            while (analizatorius.Index < analizatorius.Programa.Count())
            {
                analizatorius.Simbolis = analizatorius.Programa[analizatorius.Index];
                if (Regex.IsMatch(analizatorius.Simbolis.ToString(), "[a-zA-Z0-9]|\\s"))
                {
                    analizatorius.Zodis += analizatorius.Simbolis;
                }
                else
                {
                    analizatorius.Zodis += analizatorius.Simbolis;
                    error = true;
                }
                if (analizatorius.Peek() &&
                     analizatorius.NextChar() == '\"')
                {
                    rado = true;
                    analizatorius.Index++;
                    break;
                }

                analizatorius.Index++;
            }
            
            if (!rado) error = true;
            return error? new LentelesLeksema("klaida", analizatorius.Zodis) : new LentelesLeksema(Pavadinimas, analizatorius.Zodis);
        }
    }
    #endregion

}
