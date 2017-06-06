using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlackSys.Models.ViewModels
{
    public class ProductViewModel
    {
        public ProductModel Products { get; set; }
        public List<ProductModel> PductList { get; set; }
    }
}