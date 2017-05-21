using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Xunit;
using Newtonsoft.Json.Linq;
using ProductCatalog.Tests.EndToEnd.Setup;

namespace ProductCatalog.Tests.EndToEnd
{
	[Collection("Web API End to End Tests")]
    public class ProductsCatalogApiEndToEndTests
    {
        private readonly ApplicationFixture _app;
        public ProductsCatalogApiEndToEndTests(ApplicationFixture app)
        {
            _app = app;
        }

		[Fact]
		public void Create_new_product_and_read_it()
        {
			using(var httpClient = new HttpClient())
            {
                const string testName = "testProduct";
                const string testCategory = "tests";
                var stringContent = new StringContent($" {{ \"name\": \"{testName}\", \"productCategory\": \"{testCategory}\" }} ", Encoding.UTF8, "application/json");
                HttpResponseMessage response = httpClient.PostAsync($"{_app.ListeningEndpoint}/products", stringContent).Result;
                response.EnsureSuccessStatusCode();

                string responseContent = response.Content.ReadAsStringAsync().Result;
                Guid id = new Guid(response.Headers.Location.ToString());

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                HttpResponseMessage readResponse = httpClient.GetAsync($"{_app.ListeningEndpoint}/products/{id}").Result;
                readResponse.EnsureSuccessStatusCode();

                string readResponseContent = readResponse.Content.ReadAsStringAsync().Result;
                JObject deserializedReadResponse = JsonConvert.DeserializeObject<JObject>(readResponseContent);

                Assert.Equal(testName, deserializedReadResponse["name"].ToString());
                Assert.Equal(testCategory, deserializedReadResponse["productCategory"].ToString());
                Assert.Equal(id.ToString(), deserializedReadResponse["id"].ToString());
            }
        }
    }
}
