using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDbsMazani
{
    internal class Filamenty
    {
        public int id { get; set; }
        public string nazev { get; set; }

        public Filamenty(int id, string nazev)
        {
            this.id = id;
            this.nazev = nazev.Trim();
        }

        public override string? ToString()
        {
            return $"{nazev} číslo {id}";
        }
    }
}
