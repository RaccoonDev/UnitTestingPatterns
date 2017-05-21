using System;
using System.Threading.Tasks;

namespace ProductCatalog.Business
{
    public interface IProductsRepository
    {
        Task Save(Product product);
        Task<Product> Get(Guid id);
    }
}
