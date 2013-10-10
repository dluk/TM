namespace LeksinisAnalizatorius
{
    public static class Programa
    {
        public static string GetRegex { get { return "^(("+DeklaravimoBlokas.GetRegex + ")?" + "\\s*begin\n\\w+\nend)$"; } }
    }

    public static class DeklaravimoBlokas
    {
        public static string GetRegex { get { return "(" + Deklaracija.GetRegex + "\n?)+"; } }
    }

    public static class Deklaracija
    {
        public static string GetRegex { get { return "(" + KintamojoDeklaravimas.GetRegex + "|" + MasyvoDeklaravimas.GetRegex+")" + "\\;"; }}
    }

    public static class FunkcijosDeklaravimas
    {
        public static string GetRegex { get { return ""; }}
    }
    public static class KintamojoDeklaravimas
    {
        public static string GetRegex { get { return Tipas.GetRegex + " " + Pavadinimas.GetRegex; }}
    }
    public static class MasyvoDeklaravimas
    {
        public static string GetRegex { get { return "(array "+Tipas.GetRegex+"\\["+Skaicius.GetRegex+"\\] "+ Pavadinimas.GetRegex+")"; }}
    }
    public static class Pavadinimas
    {
        public static string GetRegex { get
        {
            return "("+Raide.GetRegex + "|" + "("+Raide.GetRegex+"+("+Raide.GetRegex+"|"+Skaitmuo.GetRegex+")*))";
        }}
    }
    public static class Tipas
    {
        public static string GetRegex { get { return "(int|string)"; }}
    }

    public static class Parametrai
    {
        public static string GetRegex { get { return ""; }}
    }
    public static class Skaicius
    {
        public static string GetRegex { get { return Skaitmuo.GetRegex + "+"; }}
    }
    public static class Raide
    {
        public static string GetRegex { get { return "[a-zA-Z]"; }}
    }
    public static class Skaitmuo
    {
        public static string GetRegex { get { return "[0-9]"; }}
    }
    //public static class Deklaracija
    //{
    //    public static string GetRegex { get { return }}
    //}
    //public static class Deklaracija
    //{
    //    public static string GetRegex { get { return }}
    //}
    //public static class Deklaracija
    //{
    //    public static string GetRegex { get { return }}
    //}


}
