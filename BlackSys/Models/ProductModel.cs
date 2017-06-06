using Blacksys.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlackSys.Models
{
    public class ProductModel : UserIdentification
    {
        [Key]
        public Int64 productid { get; set; }

        [Required]
        [Display(Name = "Category", Prompt = "Category")]
        public int categoryid { get; set; }

        [Required]
        [Display(Name = "Product", Prompt = "Product")]
        public string product { get; set; }
        public int UnitId { get; set; }

        [Required]
        [Display(Name = "Product Details", Prompt = "Product Details")]
        [DataType(DataType.MultilineText)]     
        public string ProductDetails { get; set; }

        [NotMapped]
        [Display(Name = "Units", Prompt = "Units")]
        public string Units { get; set; }

        [NotMapped]
        [Display(Name = "Category", Prompt = "Category")]
        public string CategoryName { get; set; }
        public virtual ICollection<ProductPhotosModel> ProductPhotos { get; set; }
    }
}