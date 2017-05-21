using System;
using System.Threading.Tasks;

namespace ProductCatalog.Business
{
    public class ProductCatalogManager
    {
        private readonly IProductsRepository _productsRepository;
        public ProductCatalogManager(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<Guid> AddNewProduct(Product product)
        {
            if(product.Id.HasValue)
            {
                throw new InvalidOperationException("Product id is specified. Cannot be added as new product.");
            }

            Guid newId = Guid.NewGuid();

           await _productsRepository.Save(product.SetId(newId));

            return newId;
        }

        public async Task<Product> GetProductById(Guid id)
        {
            return await _productsRepository.Get(id);
        }
    }
}
