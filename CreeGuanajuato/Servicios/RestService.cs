using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CreeGuanajuato.Models.inegi;
using CreeGuanajuato.Utilidades;
using Newtonsoft.Json;

namespace CreeGuanajuato.Servicios
{
    public class RestService : IRestService
    {
        HttpClient client;

        public RestService()
        {
            client = new HttpClient();
        }

        async Task<Estado> IRestService.ObtieneEstados()
        {
            Estado estados = new Estado();

            try
            {
                HttpResponseMessage response = null;
                var uri = new Uri(Constants.URLInegiEstados);
                response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var request = await response.Content.ReadAsStringAsync();
                    estados = JsonConvert.DeserializeObject<Estado>(request);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return estados;
        }

        async Task<Municipio> IRestService.ObtieneMunicipios(string cve_agee)
        {
            Municipio municipios = new Municipio();

            try
            {
                HttpResponseMessage response = null;

                var uri = new Uri(string.Format(Constants.URLInegiMunicipios, cve_agee));
                response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var request = await response.Content.ReadAsStringAsync();
                    municipios = JsonConvert.DeserializeObject<Municipio>(request);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return municipios;
        }

        async Task<Colonia> IRestService.ObtieneColonias(string cve_agee, string cve_agem)
        {
            Colonia colonias = new Colonia();
            string[] parameters = new string[2];
            parameters[0] = cve_agee;
            parameters[1] = cve_agem;

            try
            {
                HttpResponseMessage response = null;
                var uri = new Uri(string.Format(Constants.URLInegiColonias, parameters));
                response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var request = await response.Content.ReadAsStringAsync();
                    colonias = JsonConvert.DeserializeObject<Colonia>(request);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return colonias;
        }
    }
}
