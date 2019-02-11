using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreeGuanajuato.Models.inegi
{
    public class Estado
    {
        public EstadoDatos datos { get; set; }
    } 

    public class EstadoDatos
    {
        public string cve_agee { get; set; }
        public string nom_agee { get; set; }
    }
}
