using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COVID_19.ProductsCatalog.Web.Models
{
    public class Permissions
    {
        public bool CanEdit { get; set; }
        public bool CanAdd { get; set; }
        public bool CanDelete { get; set; }
    }
}