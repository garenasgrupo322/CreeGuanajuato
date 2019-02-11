using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreeGuanajuato.Servicios
{
    public class ServiceManager
    {
        IRestService restService;

        public ServiceManager(IRestService service)
        {
            restService = service;
        }

        #region Catalogs

        public Task<Models.inegi.Estado> ObtieneEstados()
        {
            return restService.ObtieneEstados();
        }

        public Task<Models.inegi.Municipio> ObtieneMunicipios(string cve_agee)
        {
            return restService.ObtieneMunicipios(cve_agee);
        }

        public Task<Models.inegi.Colonia> ObtieneColonias(string cve_agee, string cve_agem)
        {
            return restService.ObtieneColonias(cve_agee, cve_agem);
        }

        #endregion

    }
}
