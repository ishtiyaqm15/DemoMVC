using COVID_19.ProductsCatalog.Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace COVID_19.ProductsCatalog.Web.Models
{
    public class ProductViewModel : Permissions
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Display(Name = "Short Description")]
        [StringLength(500)]
        public string ShortDescription { get; set; }
        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }
        [Display(Name = "Image Upload")]
        [Required]
        public HttpPostedFileBase Image { get; set; }
        [Display(Name = "Price")]
        public double Price { get; set; }
        
        public string ImageUrl
        {
            get
            {
                string imreBase64Data = Convert.ToBase64String(MemoryPostedFile.GetFileBytes(Image.InputStream));
                string imgUrl = string.Format("data:image/png;base64,{0}", imreBase64Data);
                return imgUrl;
            }
        }
    }
}