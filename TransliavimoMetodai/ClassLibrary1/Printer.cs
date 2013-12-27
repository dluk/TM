using System;
using System.IO;

namespace TM.SintaksinisAnalizatorius
{
    class Printer
    {
        private SintaksinisAnalizatorius Analizatorius;
        private string pt = @"C:\Users\Gediminas\Desktop\WriteLines2.xml";
        public Printer(SintaksinisAnalizatorius analizatorius)
        {
            Analizatorius = analizatorius;
            File.Delete(pt);
        }

        public void PrintTree(Guid parent)
        {   
            Objektas obj = Analizatorius.SintaksesMedis.Find(x=>x.TevoId == parent);
            System.IO.File.AppendAllText(pt,  "<" + obj.Tipas + ">\r\n");
            if (obj.Reiksme != "")
            {
                System.IO.File.AppendAllText(pt, obj.Reiksme.Replace("<", "&lt;").Replace(">", "&gt;") + "\r\n");
            }
            while (Analizatorius.SintaksesMedis.Find(x => x.TevoId == obj.Id) != null)
            {
                PrintTree(obj.Id);
            }
            System.IO.File.AppendAllText(pt, "</" + obj.Tipas + ">\r\n");
                Analizatorius.SintaksesMedis.Remove(obj);
        }
    }
}
