using ProductCatalog.Business;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace ProductCatalog.Web.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private readonly ProductCatalogManager _catalogManager;

        public ProductsController(ProductCatalogManager catalogManager)
        {
            _catalogManager = catalogManager;
        }

        [HttpGet]
        [Route("{id}")]
        [ResponseType(typeof(ProductDto))]
        public async Task<IHttpActionResult> GetById(Guid id)
        {
            Product product = await _catalogManager.GetProductById(id);
            return Ok(new ProductDto {
                Id = product.Id,
                ProductCategory = product.ProductCategory,
                Name = product.Name,
                Currency = product.Price.Currency,
                PriceAmount = product.Price.Amount
            });
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateNewProduct(ProductDto product)
        {
            Guid id = await _catalogManager.AddNewProduct(new Product(
                id: product.Id, 
                productCategory: product.ProductCategory,
                name: product.Name,
                price: new Price(currency: product.Currency, amount: product.PriceAmount)));

            return Created(id.ToString(), "");
        }

    }

    public class ProductDto
    {
        public Guid? Id { get; set; }
        public string ProductCategory { get; set; }
        public string Name { get; set; }
        public Currency Currency { get; set; }
        public decimal PriceAmount { get; set; }
    }
}
