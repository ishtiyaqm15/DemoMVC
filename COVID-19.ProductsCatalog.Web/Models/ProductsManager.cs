using COVID_19.ProductsCatalog.Core.IRepositories;
using COVID_19.ProductsCatalog.Core.Repositories;
using System;
using System.Collections.Generic;

namespace COVID_19.ProductsCatalog.Web.Models
{
    public class ProductsManager : IDisposable
    {
        IUnitOfWork _unitOfWork;

        public ProductsManager()
        {
            _unitOfWork = new UnitOfWork(new List<Repositories>() { Repositories.ProductsRepo });
        }

        public List<ProductViewModel> GetList()
        {
            var viewModelProducts = new List<ProductViewModel>();
            var productList = _unitOfWork.ProductsRepository.GetAllProducts();
            foreach (var product in productList)
            {
                viewModelProducts.Add(new ProductViewModel { Id = product.Id, Image = product.Image,
                    LongDescription = product.LongDescription, ShortDescription = product.ShortDescription,
                    Name = product.Name, Price = product.Price
                });
            }
            return viewModelProducts;
        }

        public ProductViewModel Get(int productId)
        {
            var DomainModelProduct = _unitOfWork.ProductsRepository.GetProduct(productId);
            return new ProductViewModel
                {
                    Id = DomainModelProduct.Id,
                    Image = DomainModelProduct.Image,
                    LongDescription = DomainModelProduct.LongDescription,
                    ShortDescription = DomainModelProduct.ShortDescription,
                    Name = DomainModelProduct.Name,
                    Price = DomainModelProduct.Price
                };
        }

        public void Dispose()
        {
        }
    }
}