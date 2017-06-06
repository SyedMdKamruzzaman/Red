using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace BlackSys.Models.ViewModels
{
    public class RequisitionN1ViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Requisition Id", Prompt = "Requisition Id")]
        public string RequisitionId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Requisition Date", Prompt = "dd/MM/yyyy")]
        public DateTime RequisitionDate { get; set; }

        [Display(Name = "Item", Prompt = "Item")]
        public int Item { get; set; }

        [Display(Name = "Specification", Prompt = "Specification")]
        public string Specification { get; set; }

        [Display(Name = "Quantity", Prompt = "Quantity")]
        public decimal Quantity { get; set; }
        
        [Display(Name = "Branch", Prompt = "Branch")]
        public int BranchID { get; set; }


        [Display(Name = "Remarks", Prompt = "Remarks")]
        public string Remarks { get; set; }

        [Required()]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Approval Date", Prompt = "dd/MM/yyyy")]
        public DateTime RequisitionApprovalDate { get; set; }

        [Required()]
        [Display(Name = "Approval Status", Prompt = "ApprovalStatus")]
        public ApprovalStatus ApprovalStatus { get; set; }


        [Required()]
        [Display(Name = "Approval Quantity", Prompt = "Approval Quantity")]
        public decimal ApprovalQuantity { get; set; }


        [Required()]
        [Display(Name = "Approval Remarks", Prompt = "ApprovalRemarks")]
        public string ApprovalRemarks { get; set; }

        public List<Requisition> RequisitionList { get; set; }

        //[NotMapped]
        //public string category { get; set; }

        [NotMapped]
        public string product { get; set; }

        [NotMapped]
        public string branch { get; set; }
    }
}