using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blacksys.Controllers;

namespace BlackSys.Models.ViewModels
{
    public class AppointmentListViewModel
    {
        public Int64 BookingID { get; set; }

        public DateTime AppointmentDate { get; set; }

        public int MemberId { get; set; }

        public string Venue { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }

        public string Services { get; set; }

        [NotMapped()]
        public string ServicesID { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ServiceDate { get; set; }


        public decimal TotalServicesAmount { get; set; }

        public decimal AdvancePayment { get; set; }

        public decimal PaidAmount { get; set; }

        public string MemberName { get; set; }

        public string MemMobileNo { get; set; }

        public string MemEmail { get; set; }

        public decimal TotalDiscountAmount { get; set; }

        public decimal TotalDueAmount { get; set; }


    }
}