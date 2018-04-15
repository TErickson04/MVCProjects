using System;
using System.Collections.Generic;
using StoneStore.Domain.Entities;

namespace StoneStore.Domain.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }

        void SaveProduct(Product product);

        Product DeleteProduct(int productID);
    }
}
