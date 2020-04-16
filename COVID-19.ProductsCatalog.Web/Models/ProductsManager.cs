﻿using COVID_19.ProductsCatalog.Core.Common;
using COVID_19.ProductsCatalog.Core.DomainModels;
using COVID_19.ProductsCatalog.Core.IRepositories;
using COVID_19.ProductsCatalog.Core.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

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
                viewModelProducts.Add(new ProductViewModel { Id = product.Id, Image = new MemoryPostedFile(product.Image ?? new byte[0]),
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
                    Image = new MemoryPostedFile(DomainModelProduct.Image ?? new byte[0]),
                    LongDescription = DomainModelProduct.LongDescription,
                    ShortDescription = DomainModelProduct.ShortDescription,
                    Name = DomainModelProduct.Name,
                    Price = DomainModelProduct.Price
                };
        }

        public int Add(ProductViewModel product, string userId)
        {
            var domainModelProduct = new Product() { Name= product.Name, ShortDescription=product.ShortDescription, LongDescription=product.LongDescription, Price=product.Price };
            domainModelProduct.Image = MemoryPostedFile.GetFileBytes(product.Image.InputStream);
            domainModelProduct.CreatedBy = userId;
            var productId = _unitOfWork.ProductsRepository.AddProduct(domainModelProduct);
            return productId;
        }

        public void Dispose()
        {
        }
    }
}