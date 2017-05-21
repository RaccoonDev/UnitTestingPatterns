using System;

namespace ProductCatalog.Business
{
    public class Product
    {
        public Product(Guid? id, string productCategory, string name, Price price)
        {
            Id = id;
            ProductCategory = productCategory;
            Name = name;
            Price = price;
        }

        public Guid? Id { get; }
        public string ProductCategory { get; }
        public string Name { get; }
        public Price Price { get; }

        public Product SetId(Guid id)
        {
            return new Product(id, ProductCategory, Name, Price);
        }
    }
}
