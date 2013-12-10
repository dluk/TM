using System;
using System.Collections.Generic;
using System.Linq;
using TM.LeksinisAnalizatorius;

namespace TM.SintaksinisAnalizatorius
{
    public class SintaksinisAnalizatorius
    {
        public List<LentelesLeksema> VarduLentele;
        public List<Objektas> SintaksesMedis = new List<Objektas>();
        public int Indeksas = 0;

        public SintaksinisAnalizatorius(List<LentelesLeksema> varduLentele)
        {
            this.VarduLentele = varduLentele;
        }

        public void Analizuoti()
        {
            new DeklaravimoAnal().Analyze(this, Guid.Empty);
            Indeksas++;
            new ProgramAnalizatorius().Analyze(this, Guid.Empty);
        }

        public string PrintMedis()
        {
            string rez = "";
            foreach (Objektas objektas in SintaksesMedis)
            {
                rez += string.Format("Id: {0}, pav: {1}, reiksme {2}, tevs: {3}\r\n", objektas.Id, objektas.Tipas,
                    objektas.Reiksme, objektas.TevoId);
            }
            return rez;
        }
    }

}
