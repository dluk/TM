using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.SintaksinisAnalizatorius
{
    class Printer
    {
        private SintaksinisAnalizatorius Analizatorius;
        private System.IO.StreamWriter file;
        public Printer(SintaksinisAnalizatorius analizatorius)
        {
            Analizatorius = analizatorius;
        }

        public void PrintTree(Guid parent, string space)
        {   
            Objektas obj = Analizatorius.SintaksesMedis.Find(x=>x.TevoId == parent);
            System.IO.File.AppendAllText(@"C:\Users\Gediminas\Desktop\WriteLines2.txt", space + "<" + obj.Tipas + ">\r\n");
            while (Analizatorius.SintaksesMedis.Find(x => x.TevoId == obj.Id) != null)
            {
                PrintTree(obj.Id, space + "   ");
            }
            if (obj.Reiksme != "")
            {
                System.IO.File.AppendAllText(@"C:\Users\Gediminas\Desktop\WriteLines2.txt",  space + obj.Reiksme + "\r\n");
            }
                System.IO.File.AppendAllText(@"C:\Users\Gediminas\Desktop\WriteLines2.txt", space + "</" + obj.Tipas + ">\r\n");
                Analizatorius.SintaksesMedis.Remove(obj);
        }
    }
}
