
using COVID_19.ProductsCatalog.Core.DomainModels;
using COVID_19.ProductsCatalog.Core.Repositories;
using COVID_19.ProductsCatalog.Core.Tests.Integration;
using NUnit.Framework;
using System;
using System.Linq;

namespace COVID_19.ProductsCatalog.Core.Tests
{
    [TestFixture]
    [IntegrationTest]
    public class ProductsRepositoryTests : SqlServerIntegrationTestBase
    {
        [SetUp]
        protected void Setup()
        {
            TearDown();
            base.ExecuteSqlScript(@"ProductsSetup.sql");
        }

        [TearDown]
        protected void TearDown()
        {
            base.ExecuteSqlScript(@"ProductsTearDown.sql");
        }


        [Test]
        public void GetProducts_ShouldReturn_TwoProducts()
        {
            // Arrange
            var repository = new ProductsRepository();

            // Act
            var products = repository.GetAllProducts();

            // Assert
            Assert.AreEqual(2, products.Count);
            Assert.AreEqual("Gloves", products.ElementAt(0).Name);
            Assert.AreEqual("Mask", products.ElementAt(1).Name);
        }

        [Test]
        public void GetProduct_ShouldReturn_FullProductInformation()
        {
            // Arrange
            var repository = new ProductsRepository();
            var productId = 1;

            // Act
            var product = repository.GetProduct(productId);

            // Assert
            Assert.AreEqual("Mask", product.Name);
            Assert.AreEqual(productId, product.Id);
            Assert.AreEqual("Health Equipment", product.LongDescription);
            Assert.AreEqual("Health Equipment", product.ShortDescription);
            Assert.AreEqual(2, product.Price);
            Assert.AreEqual(true, product.IsActive);
        }

        [Test]
        public void AddProduct_ShouldAdd_VerifyId()
        {
            // Arrange
            var repository = new ProductsRepository();
            var tempProduct = repository.GetProduct(1);//To get valid userId
            var product = new Product() { Name = "Ventilator", ShortDescription = "Oxygen supplies", LongDescription = "Oxygen supplies", Price = 200, IsActive = true, CreatedOn = DateTime.Now, CreatedBy = tempProduct.CreatedBy };

            // Act
            var productId = repository.AddProduct(product);

            // Assert
            Assert.IsTrue(productId > 0);
        }


        [Test]
        public void UpdateProduct_ShouldUpdate_VerifyInformation()
        {
            // Arrange
            var repository = new ProductsRepository();
            var tempProduct = repository.GetProduct(1);
            var product = new Product()
            {
                Id = tempProduct.Id,
                ShortDescription = "Oxygen supplies and misc",
                LongDescription = "Oxygen supplies and misc",
                Name = "New Mask",
                Price = 8,
                UpdatedBy = tempProduct.CreatedBy,
                IsActive= true
            };

            // Act
            var updated = repository.UpdateProduct(product);

            // Assert
            Assert.IsTrue(updated);
            Assert.AreNotEqual(product.ShortDescription, tempProduct.ShortDescription);
            Assert.AreNotEqual(product.Name, tempProduct.Name);
            Assert.AreNotEqual(product.Price, tempProduct.Price);
            Assert.IsNotNull(product.UpdatedBy);
        }

        [Test]
        public void DeleteProduct_ShouldDelete_VerifyProductNotExists()
        {
            // Arrange
            var repository = new ProductsRepository();

            // Act
            var deleted = repository.DeleteProduct(1);
            var product = repository.GetProduct(1);


            // Assert
            Assert.IsTrue(deleted);
            Assert.IsNull(product);
        }
    }
}

