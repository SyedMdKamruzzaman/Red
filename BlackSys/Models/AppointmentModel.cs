using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.WebPages.Html;
using Blacksys.Controllers;

namespace BlackSys.Models
{
     
    public class Appointment : UserIdentification
    {
       
        [Key]
        public Int64 BookingID { get; set; }

        
       
        [Required()]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Apointment Date", Prompt = "dd-MMM-yyyy")]
        public DateTime AppointmentDate
        { get; set; }

        [Required()]
        [Display(Name = "Member", Prompt = "Member")]
        public int MemberId { get; set; }
        
        [Display(Name = "Venue", Prompt = "Venue")]
        public string Venue { get; set; }

        [Required()]
        [Display(Name = "Branch", Prompt = "Branch")]
        public int BranchId { get; set; }

        [Required()]
        [Display(Name = "Services", Prompt = "Services")]
        public string Services { get; set; }

        [NotMapped()]        
        public string ServicesID { get; set; }

        [Required()]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Service Date", Prompt = "dd/MM/yyyy")]
        public DateTime ServiceDate { get; set; }


        [Required()]
        [Display(Name = "Total Services Amount", Prompt = "Total Services Amount")]
        public decimal TotalServicesAmount { get; set; }

      
        [Display(Name = "Advance Payment", Prompt = "Advance Payment")]
        public Nullable<decimal> AdvancePayment { get; set; }

        [DefaultValue(0)]
        public bool IsCompleted { get; set; }

        [DefaultValue(0)]
        public bool IsCanceled { get; set; }

        public Nullable<DateTime> CancelledDate { get; set; }

    }   
}