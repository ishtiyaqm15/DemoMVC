using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COVID_19.ProductsCatalog.Web.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int ShortDescription { get; set; }
        public int LongDescription { get; set; }
        public byte[] Image { get; set; }
        public byte[] Price { get; set; }
    }
}