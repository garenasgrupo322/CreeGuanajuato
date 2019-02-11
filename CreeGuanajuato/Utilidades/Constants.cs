using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreeGuanajuato.Utilidades
{
    public class Constants
    {
        /// <summary>
        /// URlS para la consulta sobre api de inegi
        /// </summary>
        public static string URLInegiEstados = "http://geoweb2.inegi.org.mx/wscatgeo/mgee/11";
        public static string URLInegiMunicipios = "http://geoweb2.inegi.org.mx/wscatgeo/mgem/{0}";
        public static string URLInegiColonias = "http://geoweb2.inegi.org.mx/wscatgeo/localidades/{0}/{1}";
    }
}
