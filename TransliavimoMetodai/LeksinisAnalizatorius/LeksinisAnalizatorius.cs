using System.Text.RegularExpressions;

namespace LeksinisAnalizatorius
{
    public class Analizatorius
    {
        private string _failas;


        public Analizatorius(string failas)
        {
            _failas = failas;
        }

        public string KitaLitera()
        {
            var x = Regex.Match(_failas, Programa.GetRegex);
            return x.ToString();
        }
    }
}
