using COVID_19.ProductsCatalog.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace COVID_19.ProductsCatalog.Web.Controllers
{
    public class ProductsController : BaseController
    {
        public ActionResult Index(int pageNum = 1)
        {
            using (var productsManager = new ProductsManager())
            {
                var productsPaginatedModel = new ProductsPaginatedModel();
                productsPaginatedModel.Products = productsManager.GetList();
                productsPaginatedModel.CurrentPage = pageNum;
                productsPaginatedModel.ProductPerPage = 10;
                return View(productsPaginatedModel);
            }
        }
        
        public ActionResult Details(int id)
        {
            return View();
        }
        
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(ProductViewModel product)
        {
            var productId = 0;
            using (var productsManager = new ProductsManager())
            {
                productId = productsManager.Add(product, User.Identity.GetUserId());
            }

            return RedirectToAction("Index");
        }
        
        public ActionResult Edit(int id)
        {
            using (var productsManager = new ProductsManager())
            {
                var product = productsManager.Get(id);
                return View(product);
            }
        }
        
        [HttpPost]
        public ActionResult Edit(ProductViewModel product)
        {
            using (var productsManager = new ProductsManager())
            {
                productsManager.Update(product, User.Identity.GetUserId());
            }
            return RedirectToAction("Edit", new { id = product.Id});
        }

        [HttpPost]
        public string Delete(int id)
        {
            var productName = "";
            using (var productsManager = new ProductsManager())
            {
                productName = productsManager.Get(id).Name;
                productsManager.Delete(id);
            }
            return string.Format("Product '{0}' successfully deleted!", productName);
        }
    }
}
