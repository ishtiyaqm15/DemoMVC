using COVID_19.ProductsCatalog.Core.Security;
using COVID_19.ProductsCatalog.Web.App_Start;
using COVID_19.ProductsCatalog.Web.Models;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace COVID_19.ProductsCatalog.Web.Controllers
{
    public class ProductsController : BaseController
    {
        public ActionResult Index(int pageNum = 1)
        {
            using (var productsManager = new ProductsManager())
            {
                var productsPaginatedModel = new ProductsPaginatedModel
                {
                    Products = productsManager.GetList(),
                    CurrentPage = pageNum,
                    ProductPerPage = 10,
                    CanEdit = CanEdit,
                    CanAdd = CanAdd
                };
                return View(productsPaginatedModel);
            }
        }

        public ActionResult Details(int id)
        {
            using (var productsManager = new ProductsManager())
            {
                var product = productsManager.Get(id);
                product.CanEdit = CanEdit;
                product.CanAdd = CanAdd;
                return View(product);
            }
        }

        [AuthorizeFilter(new string[] {"Admin"})]
        public ActionResult Create()
        {
            return View();
        }

        [AuthorizeFilter(new string[] { "Admin" })]
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

        [AuthorizeFilter(new string[] { "Admin", "Content Contributors" })]
        public ActionResult Edit(int id)
        {
            using (var productsManager = new ProductsManager())
            {
                var product = productsManager.Get(id);
                return View(product);
            }
        }

        [AuthorizeFilter(new string[] { "Admin", "Content Contributors" })]
        [HttpPost]
        public ActionResult Edit(ProductViewModel product)
        {
            using (var productsManager = new ProductsManager())
            {
                productsManager.Update(product, User.Identity.GetUserId());
            }
            return RedirectToAction("Edit", new { id = product.Id});
        }

        [AuthorizeFilter(new string[] { "Admin", "Content Contributors" })]
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
