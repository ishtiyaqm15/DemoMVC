using COVID_19.ProductsCatalog.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVID_19.ProductsCatalog.Core.IRepositories
{
    public interface IProductsRepository
    {
        IList<Product> GetAllProducts(bool isActive = true);
        Product GetProduct(int productId);
        bool UpdateProduct(Product product);
        int AddProduct(Product product);
        bool DeleteProduct(int productId);
    }
}
