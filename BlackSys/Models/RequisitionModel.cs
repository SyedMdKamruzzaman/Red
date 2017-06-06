using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
namespace BlackSys.Models
{
    public class Requisition 
    {
        [Key]
        public int ID { get; set; }

        [Required()]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Requisition Date", Prompt = "dd/MM/yyyy")]
        public DateTime RequisitionDate { get; set; }

        [Required()]
        [Display(Name = "Item", Prompt = "Item")]
        public int Item { get; set; }

        [Required()]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Specification", Prompt = "Specification")]
        public string Specification { get; set; }

        [Required()]
        [Display(Name = "Quantity", Prompt = "Quantity")]
        public decimal Quantity { get; set; }

        [Required()]
        [Display(Name = "Branch", Prompt = "Branch")]
        public int BranchID { get; set; }

        
        
        [DataType(DataType.MultilineText)]
        [Display(Name = "Remarks", Prompt = "Remarks")]
        public string Remarks { get; set; }

        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Approval Date", Prompt = "dd/MM/yyyy")]
        public DateTime RequisitionApprovalDate { get; set; }
        
        [Display(Name = "Approval Status", Prompt = "ApprovalStatus")]
        public ApprovalStatus ApprovalStatus { get; set; }


        
        [Display(Name = "Approval Quantity", Prompt = "Approval Quantity")]
        public int ApprovalQuantity { get; set; }

        
        [Display(Name = "Approval Remarks", Prompt = "ApprovalRemarks")]
        public string ApprovalRemarks { get; set; }

        //[NotMapped]
        //public string category { get; set; }

        [NotMapped]
        public string product { get; set; }

        [NotMapped]
        public string branch { get; set; }
    }


    public enum ApprovalStatus
    {
        [Description("Not Approved")] NotApproved,
        Viewed,
        Approved,
        Rejected
    }

       
}