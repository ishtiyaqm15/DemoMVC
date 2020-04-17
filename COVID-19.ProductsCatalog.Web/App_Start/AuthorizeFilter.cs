using COVID_19.ProductsCatalog.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace COVID_19.ProductsCatalog.Web.App_Start
{
    public class AuthorizeFilter : ActionFilterAttribute, IActionFilter
    {
        public AuthorizeFilter(params string[] roles)
        {
            this.Roles = roles;
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Roles.Count() > 0)
            {
                var controller = (BaseController)filterContext.Controller;
                var currentRoles = controller.CurrentRoles;

                if (currentRoles != null && !currentRoles.Intersect(Roles).Any())
                {
                    filterContext.Result = controller.AccessDenied();
                }
            }
        }

        public string[] Roles { get; set; }
    }
}