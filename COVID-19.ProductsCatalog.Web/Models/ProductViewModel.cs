using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COVID_19.ProductsCatalog.Web.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public byte[] Image { get; set; }
        public double Price { get; set; }
    }
}