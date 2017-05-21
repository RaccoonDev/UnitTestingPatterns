using System;
using ProductCatalog.Business;
using Xunit;
using Moq;
using System.Threading.Tasks;

namespace ProductCatalog.Tests
{
    public class ProductCatalogManager_should
    {
        [Fact]
        public async Task Add_new_product_to_catalog()
        {
            // Arrange
            var productsRepositoryMock = new Mock<IProductsRepository>();
            var productCatalogManager = new ProductCatalogManager(productsRepositoryMock.Object);
            var product = new Product(
                    id: null,
                    productCategory: "Electornics",
                    name: "kindle",
                    price: new Price(
                        currency: Currency.USDollars,
                        amount: 75)
            );

            // Act
            await productCatalogManager.AddNewProduct(product);

            // Assert
            productsRepositoryMock
                .Verify(m => m.Save(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task Not_add_already_existing_product_as_new_one()
        {
            // Arrange
            var productsRepositoryMock = new Mock<IProductsRepository>();
            var productCatalogManager = new ProductCatalogManager(productsRepositoryMock.Object);
            var product = new Product(
                    id: new Guid("7d490273-ff99-474c-b86d-220de67776d3"),
                    productCategory: "Electornics",
                    name: "kindle",
                    price: new Price(
                        currency: Currency.USDollars,
                        amount: 75)
            );

            // Act
            InvalidOperationException ex = await Assert.ThrowsAsync<InvalidOperationException>
                (async () => await productCatalogManager.AddNewProduct(product));

            // Assert
            Assert.Equal("Product id is specified. Cannot be added as new product.", ex.Message);
        }

        [Fact]
        public async Task Return_newly_created_product_id()
        {
            // Arrange
            var productsRepositoryMock = new Mock<IProductsRepository>();
            var productCatalogManager = new ProductCatalogManager(productsRepositoryMock.Object);
            var product = new Product(
                    id: null,
                    productCategory: "Electornics",
                    name: "kindle",
                    price: new Price(
                        currency: Currency.USDollars,
                        amount: 75)
            );

            // Act
            Guid id = await productCatalogManager.AddNewProduct(product);

            // Assert
            productsRepositoryMock
                .Verify(m => m.Save(It.IsAny<Product>()), Times.Once);
            Assert.NotEqual(Guid.Empty, id);
        }
    }
}
