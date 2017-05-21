using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProductCatalog.Business;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProductCatalog.DataAccess
{
    public class ElasticsearchProductRepository : IProductsRepository
    {
        private readonly string _elasticsearchEndpoint;
        public ElasticsearchProductRepository(string elasticsearchEndpoint)
        {
            _elasticsearchEndpoint = elasticsearchEndpoint.TrimEnd('/');
        }
        public async Task Save(Product product)
        {
            using(var httpClient = new HttpClient())
            {
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(product));
                HttpResponseMessage result = await httpClient.PutAsync($"{_elasticsearchEndpoint}/productscatalog/products/{product.Id}", stringContent);
                result.EnsureSuccessStatusCode();
            }
        }

        public async Task<Product> Get(Guid id)
        {
            using(var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync($"{_elasticsearchEndpoint}/productscatalog/products/{id}");
                string content = await response.Content.ReadAsStringAsync();
                JObject jObject = JsonConvert.DeserializeObject<JObject>(content);
                return JsonConvert.DeserializeObject<Product>(jObject["_source"].ToString());
            }
        }
    }
}
