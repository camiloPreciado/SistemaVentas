using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace inventario.api.Utils
{
    public class ProductoClient
    {
        private readonly string _urlBase = "http://productos.api/api/productos";

        public async Task<bool> ProductoExiste(int id)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(5);

                client.DefaultRequestHeaders.Add("x-api-key", "conectionTest");

                var response = await client.GetAsync($"{_urlBase}/{id}");
                return response.IsSuccessStatusCode;
            }
        }
    }
}
