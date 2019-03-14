using System;
using System.Threading.Tasks;
using CreeGuanajuato.Models;

namespace CreeGuanajuatoApi.Services
{
    public interface IRestService
    {
        Task<GoogleAPI> ObtieneCoordenadas(string direccion);
    }
}
