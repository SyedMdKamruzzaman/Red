using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Blacksys.Controllers;

namespace BlackSys.Models
{
    public class RequisitionItemCategoryModel : UserIdentification
    {
        [Key]
        public int ID { get; set; }

        [Required()]
        [Display(Name = "Category", Prompt = "Category")]
        public string Category { get; set; }
    }
}