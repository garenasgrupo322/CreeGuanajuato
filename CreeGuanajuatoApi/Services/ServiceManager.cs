using System;
using System.Threading.Tasks;
using CreeGuanajuato.Models;

namespace CreeGuanajuatoApi.Services
{
    public class ServiceManager
    {
        IRestService restService;

        public ServiceManager(IRestService service)
        {
            restService = service;
        }

        public Task<GoogleAPI> ObtieneCoordenadas(string direccion)
        {
            return restService.ObtieneCoordenadas(direccion);
        }
    }
}
