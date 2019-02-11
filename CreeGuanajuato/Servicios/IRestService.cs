using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreeGuanajuato.Servicios
{
    public interface IRestService
    {
        #region Catalogs
        Task<Models.inegi.Estado> ObtieneEstados();
        Task<Models.inegi.Municipio> ObtieneMunicipios(string cve_agee);
        Task<Models.inegi.Colonia> ObtieneColonias(string cve_agee, string cve_agem);
        #endregion
    }
}
