using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blacksys.Controllers;

namespace BlackSys.Models
{
    public class ProductSales
    {
        [Key]
        public Int64 Id { get; set; }

        public int AccountsCategoryId { get; set; }

        public Int64 ItemId { get; set; }

        public int Quantity { get; set; }

        public int UnitId { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal SubTotal { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Sales Date", Prompt = "dd-MMM-yyyy")]
        public DateTime SalesDate { get; set; }

        public int BranchId { get; set; }


    }
}