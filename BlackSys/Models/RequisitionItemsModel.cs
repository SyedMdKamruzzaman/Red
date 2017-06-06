using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Blacksys.Controllers;


namespace BlackSys.Models
{
    public class RequisitionItems : UserIdentification
    {
        [Key]
        public int ID { get; set; }

        [Required()]
        [Display(Name = "Category", Prompt = "Category")]
        public Int16 Category { get; set; }


        [Required()]
        [Display(Name = "Item", Prompt = "Item")]
        public string Item { get; set; }
    }
}