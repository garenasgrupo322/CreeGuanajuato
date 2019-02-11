using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreeGuanajuato.Models.inegi
{
    public class Municipio
    {
        public List<MunicipioDatos> datos { get; set; }
    }

    public class MunicipioDatos
    {
        public string cve_agem { get; set; }
        public string nom_agem { get; set; }
    }
}
