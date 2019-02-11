using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreeGuanajuato.Models.inegi
{
    public class Colonia
    {
        public List<ColoniaDatos> datos { get; set; }
    }

    public class ColoniaDatos
    {
        public string cve_loc { get; set; }
        public string nom_loc { get; set; }
    }
}
