using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blacksys.Controllers;

namespace BlackSys.Models
{
    public class StockOutModel:UserIdentification
    {
        [Key]
        public int stockoutid { get; set; }

        [Required()]
        [Display(Name = "Category", Prompt = "Category")]
        public int categoryid { get; set; }

        [Required()]
        [Display(Name = "Product", Prompt = "Product")]
        public int productid { get; set; }

        [Required()]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Stock Out Date", Prompt = "dd/MM/yyyy")]
        public DateTime stockoutdate { get; set; }

        [Required()]
        [Display(Name = "Quantity", Prompt = "Quantity")]
        public decimal quantity { get; set; }

        [Required()]
        [Display(Name = "Branch", Prompt = "Branch")]
        public int branchid { get; set; }

        [Required()]
        [Display(Name = "Deaprtment", Prompt = "Department")]
        public int depid { get; set; }

        [Required()]
        [Display(Name = "Employee", Prompt = "Employee")]
        public int empid { get; set; }

        [NotMapped]
        [Display(Name = "Branch", Prompt = "Branch")]
        public string branch { get; set; }


        [NotMapped]
        [Display(Name = "Department", Prompt = "Department")]
        public string department { get; set; }

        
        [NotMapped]
        [Display(Name = "Category", Prompt = "Category")]
        public string category { get; set; }

        
        [NotMapped]
        [Display(Name = "Product", Prompt = "Product")]
        public string product { get; set; }

        [NotMapped]
        [Display(Name = "Employee", Prompt = "Employee")]
        public string employee { get; set; }

    }
}