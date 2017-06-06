using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blacksys.Controllers;
using System.Web.Mvc;


namespace BlackSys.Models.ViewModels
{
    public class PaymentReportViewModel
    {

        public Int64 BookingID { get; set; }

        public Int64 JobID { get; set; }

        public Int64 ServiceID { get; set; }

        public Decimal ServiceRate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ServiceDate { get; set; }

        public Int64 Beautician { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime AppointmentDate { get; set; }
        public string Venue { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EntryDateTime { get; set; }

        public int CardTypeId { get; set; }

        public string CardType { get; set; }

        public string MemberName { get; set; }

        public string MemMobileNo { get; set; }

        public string MemEmail { get; set; }

        public decimal TotalServiceAmount { get; set; }

        public decimal BookingPayment { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PaymentDate { get; set; }

        public decimal Due { get; set; }

        public string SlipNo { get; set; }

        public string EmpName { get; set; }

        public string BranchName { get; set; }

        public string BranchAddress { get; set; }

        public string EntryBy { get; set; }

        public decimal AdvancePayment { get; set; }
        public decimal TotalPayableAmount { get; set; }
        public string PaymentTerms { get; set; }
        [NotMapped]
        public string ServiceName { get; set; }

        public ServicePayment servicePayment { get; set; }

        [Display(Name = "Pay Amount")]
        public decimal PayAmount { get; set; }
    }
}