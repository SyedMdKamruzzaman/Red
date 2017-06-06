using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blacksys.Controllers;

namespace BlackSys.Models
{
    public class PurchaseOrder:UserIdentification
    {
        [Key]
        public int ID { get; set; }

        public int ReqID { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Requisition Date", Prompt = "dd/MM/yyyy")]
        public DateTime RequisitionDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Purchase Date", Prompt = "dd/MM/yyyy")]
        public DateTime PurchaseDate { get; set; }

        [Display(Name = "Item", Prompt = "Item")]
        public Int64 Item { get; set; }


        [DataType(DataType.MultilineText)]
        [Display(Name = "Specification", Prompt = "Specification")]
        public string Specification { get; set; }


        [Display(Name = "Quantity", Prompt = "Quantity")]
        public decimal Quantity { get; set; }


       [NotMapped]
        public string Unit { get; set; }
        public int UnitId { get; set; }

        [Display(Name = "Unit Price", Prompt = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "SubTotal", Prompt = "SubTotal")]
        public decimal SubTotal { get; set; }

        [Display(Name = "Branch", Prompt = "Branch")]
        public int BranchID { get; set; }



        [DataType(DataType.MultilineText)]
        [Display(Name = "Remarks", Prompt = "Remarks")]
        public string Remarks { get; set; }


        [Display(Name = "Approval Quantity", Prompt = "Approval Quantity")]
        public decimal ApprovalQuantity { get; set; }


        [Display(Name = "Approval Remarks", Prompt = "ApprovalRemarks")]
        [DataType(DataType.MultilineText)]
        public string ApprovalRemarks { get; set; }

        [NotMapped]
        public string VendorName { get; set; }

        [Display(Name = "Vendor", Prompt = "Vendor")]
        public int VendorId { get; set; }


        [NotMapped]
        public decimal PurchasedQuantity { get; set; }


        [NotMapped]
        public string product { get; set; }

        [NotMapped]
        public string branch { get; set; }

        [NotMapped]
        [Display(Name = "Save in Stock", Prompt = "Save in Stock")]
        public bool IsChecked { get; set; }
    }
}