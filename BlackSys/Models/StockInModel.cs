using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blacksys.Controllers;
using System.Web.Mvc;


namespace BlackSys.Models
{
    public class StockInModel : UserIdentification
    {
        [Key]
        public int stockinid { get; set; }

        [Required]
        [Display(Name = "Product", Prompt = "Product")]
        public int productid { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Stock In Date", Prompt = "dd/MM/yyyy")]
        public DateTime stocktakedate { get; set; }

        [Required]
        [Display(Name = "Quantity", Prompt = "Quantity")]
        public decimal quantity { get; set; }

        [Required]
        [Display(Name = "Branch", Prompt = "Branch")]
        public int branchid { get; set; }

        [Required]
        [Display(Name = "InvoiceNo", Prompt = "Invoice No")]
        public int invoiceno { get; set; }

        [Required]
        [Display(Name = "Condition", Prompt = "Condition")]
        public int condition { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Remarks", Prompt = "Remarks")]
        public string remarks { get; set; }

        public int ReqID
        {
            get; set;
        }
        public int UnitID
        {
            get; set;
        }


        [NotMapped]
        public string product { get; set; }

        [NotMapped]
        public string branch { get; set; }

        [NotMapped]
        public string UnitName { get; set; }
        [NotMapped]
        public string ConditionName { get; set; }

    }
}