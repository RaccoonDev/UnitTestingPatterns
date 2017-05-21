using ProductCatalog.Business;
using ProductCatalog.DataAccess;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProductCatalog.Tests.Integration
{
    public class ProductRepositoryIntegrationTests
    {
        [Fact]
        public async Task Should_save_given_product_to_elasticsearch_and_retrieve_it()
        {
            var esProductRepository = new ElasticsearchProductRepository(elasticsearchEndpoint: "http://localhost:9200");
            Guid id = Guid.NewGuid();
            var writeProduct = new Product(id, "Electornics", "kindle", new Price(Currency.USDollars, 75));
            await esProductRepository.Save(writeProduct);

            Product readInstance = await esProductRepository.Get(id);

            Assert.Equal(writeProduct.Id, readInstance.Id);
            Assert.Equal(writeProduct.ProductCategory, readInstance.ProductCategory);
            Assert.Equal(writeProduct.Name, readInstance.Name);
            Assert.Equal(writeProduct.Price.Currency, readInstance.Price.Currency);
            Assert.Equal(writeProduct.Price.Amount, readInstance.Price.Amount);
        }
    }
}
