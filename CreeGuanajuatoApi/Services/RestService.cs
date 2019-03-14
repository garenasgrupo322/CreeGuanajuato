using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using CreeGuanajuato.Models;
using CreeGuanajuato.Utils;
using Newtonsoft.Json;

namespace CreeGuanajuatoApi.Services
{
    public class RestService : IRestService
    {
        HttpClient client;

        public RestService()
        {
            client = new HttpClient();
        }

        public async Task<GoogleAPI> ObtieneCoordenadas(string direccion)
        {
            GoogleAPI googleAPI = new GoogleAPI();

            try
            {
                HttpResponseMessage response = null;
                var uri = new Uri("https://maps.googleapis.com/maps/api/geocode/json?address=" + direccion + "&sensor=false&key=" + Constants.GoogleAPI);
                response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var request = await response.Content.ReadAsStringAsync();
                    googleAPI = JsonConvert.DeserializeObject<GoogleAPI>(request);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return googleAPI;
        }
    }
}
