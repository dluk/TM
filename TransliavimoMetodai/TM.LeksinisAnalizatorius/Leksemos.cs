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
            bool rado = false;
            bool error = false;
            analizatorius.Zodis += analizatorius.Simbolis[0];
            while (analizatorius.Reader.Read(analizatorius.Simbolis, 0, 1) > 0)
            {
                if (analizatorius.Simbolis[0] == ' ' || analizatorius.Simbolis[0] == '\n')
                {
                    rado = true;
                    break;
                }
                else
                {
                    if (Regex.IsMatch(analizatorius.Simbolis[0].ToString(), "[a-zA-Z0-9]"))
                    {
                        analizatorius.Zodis += analizatorius.Simbolis[0];
                    }
                    else
                    {
                        var first = analizatorius.Leksemos.FirstOrDefault(x => x.Tinka(analizatorius.Simbolis[0]));
                        if (first != null)
                        {
                            if (error)
                            {
                                analizatorius.VarduLentele.Add(new LentelesLeksema("error3", analizatorius.Zodis));
                            }
                            else
                            {
                            //    if(analizatorius.Zodis == "array")
                            //        analizatorius.VarduLentele.Add(new LentelesLeksema("Masyvas","array"));
                            //    else if(analizatorius.Zodis == "string")
                            //        analizatorius.VarduLentele.Add(new LentelesLeksema("Eilute", "string"));
                            //    else if(analizatorius.Zodis == "begin")
                            //        analizatorius.VarduLentele.Add(new LentelesLeksema("begin", "begin"));
                            //    else if(analizatorius.Zodis == "end")
                            //        analizatorius.VarduLentele.Add(new LentelesLeksema("end", "end"));
                            //    else
                                    analizatorius.VarduLentele.Add(new LentelesLeksema(Pavadinimas, analizatorius.Zodis));
                            }
                            analizatorius.VarduLentele.Add(first.Analize(analizatorius));
                            return null;
                        }
                        else
                        {
                            error = true;

                        }
                        
                    }

                }

            }
            if (rado)
            {
                if (error) return new LentelesLeksema("error", analizatorius.Zodis);
                return new LentelesLeksema(Pavadinimas, analizatorius.Zodis);
            }
            else
            {
                return null;
            }
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
            bool rado = false;
            bool error = false;
            analizatorius.Zodis += analizatorius.Simbolis[0];
            while (analizatorius.Reader.Read(analizatorius.Simbolis, 0, 1) > 0)
            {
                if (analizatorius.Simbolis[0] == ' ' || analizatorius.Simbolis[0] == '\n')
                {
                    rado = true;
                    break;
                }
                else
                {
                    int sk;
                    if (int.TryParse(analizatorius.Simbolis[0].ToString(), out sk))
                    {
                        analizatorius.Zodis += analizatorius.Simbolis[0];
                    }
                    else if (Regex.IsMatch(analizatorius.Simbolis[0].ToString(), "[a-zA-Z]"))
                    {
                        analizatorius.Zodis += analizatorius.Simbolis[0];
                        error = true;
                    }
                    else
                    {
                        var first = analizatorius.Leksemos.FirstOrDefault(x => x.Tinka(analizatorius.Simbolis[0]));
                        if (first != null)
                        {
                            if (!error) 
                                analizatorius.VarduLentele.Add(new LentelesLeksema(Pavadinimas, analizatorius.Zodis));
                            else
                            {
                                analizatorius.VarduLentele.Add(new LentelesLeksema("error2", analizatorius.Zodis));
                            }
                            analizatorius.VarduLentele.Add(first.Analize(analizatorius));
                            return null;
                        }
                        else
                        {
                            error = true;

                        }
                        

                    }
                   
                }

            }
            if (rado)
            {
                if (error) return new LentelesLeksema("error", analizatorius.Zodis);
                return new LentelesLeksema(Pavadinimas, analizatorius.Zodis);
            }
            else
            {
                return null;
            }
        }
    }

    public class Kabutes : ILeksema
    {
        public string Pavadinimas { get { return "Konstanta"; } }
        public bool Tinka(char charas)
        {
            return charas == '"';
        }

        public LentelesLeksema Analize(LeksinisAnalizatorius analizatorius)
        {
            bool rado = false;
            bool blogas = false;
            while (analizatorius.Reader.Read(analizatorius.Simbolis, 0, 1) > 0)
            {
                if (analizatorius.Simbolis[0] == '"')
                {
                    rado = true;
                    break;
                }
                else
                {
                    if (Regex.IsMatch(analizatorius.Simbolis[0].ToString(), "[0-9a-zA-Z]|\\s"))
                        analizatorius.Zodis += analizatorius.Simbolis[0];
                    else
                    {
                        blogas = true;
                    }
                }

            }
            if (rado)
            {
                if (blogas) return new LentelesLeksema("error", analizatorius.Zodis);
                return new LentelesLeksema(Pavadinimas, analizatorius.Zodis);
            }
            else
            {
                return null;
            }

        }
    }

}
