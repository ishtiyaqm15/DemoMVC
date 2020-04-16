using COVID_19.ProductsCatalog.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using COVID_19.ProductsCatalog.Core.DomainModels;
using System.Data;
using System.Linq;

namespace COVID_19.ProductsCatalog.Core.Repositories
{
    public class ProductsRepository : BaseRepository, IProductsRepository
    {
        public int AddProduct(Product product)
        {
            var param = new {@Name = product.Name, @ShortDescription = product.ShortDescription, @LongDescription = product.LongDescription, @Image = product.Image, @Price = product.Price, @CreatedBy = product.CreatedBy};
            return base.ExecuteAndGetAsInteger("[dbo].[sp_AddProduct]", param, CommandType.StoredProcedure);
        }

        public bool DeleteProduct(int productId)
        {
            var param = new { @productId = productId };
            return base.ExecuteAndGetAsBoolean("[dbo].[sp_DeleteProduct]", param, CommandType.StoredProcedure);
        }

        public bool DeleteProduct(int productId, bool isActive)
        {
            throw new NotImplementedException();
        }

        public IList<Product> GetAllProducts(bool isActive = true)
        {
            return base.ExecuteAndGetAsObject<Product>("[dbo].[sp_GetAllProducts]", new { @isActive = isActive }, CommandType.StoredProcedure).ToList();
        }

        public Product GetProduct(int productId)
        {
            return base.ExecuteAndGetAsObject<Product>("[dbo].[sp_GetProductById]", new { @productId = productId }, CommandType.StoredProcedure).FirstOrDefault();
        }

        public bool UpdateProduct(Product product)
        {
            var param = new { @Name = product.Name, @productId = product.Id, @ShortDescription = product.ShortDescription, @LongDescription = product.LongDescription, @Image = product.Image, @Price = product.Price, @UpdatedBy = product.UpdatedBy };
            return base.ExecuteAndGetAsBoolean("[dbo].[sp_UpdateProduct]", param, CommandType.StoredProcedure);
        }
    }
}
