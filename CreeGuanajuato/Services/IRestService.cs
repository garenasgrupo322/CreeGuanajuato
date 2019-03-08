using System;
using System.Threading.Tasks;
using CreeGuanajuato.Models;

namespace CreeGuanajuato.Services
{
    public interface IRestService
    {
        Task<GoogleAPI> ObtieneCoordenadas(string direccion);
    }
}
