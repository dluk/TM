using System.Collections.Generic;
using System.Linq;
using TM.LeksinisAnalizatorius;

namespace TM.SintaksinisAnalizatorius
{
    public class SintaksinisAnalizatorius
    {
        public List<LentelesLeksema> VarduLentele;

        public SintaksinisAnalizatorius(List<LentelesLeksema> varduLentele)
        {
            this.VarduLentele = varduLentele;
        }

        public void Analizuoti()
        {
            if (VarduLentele.First().Reiksme != "begin")
            {

            }
        }
    }

}
