using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using Blacksys.Controllers;
using System.ComponentModel;

namespace BlackSys.Models
{
    public class RePayment
    {
        [Key]
        public int ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Payment Date", Prompt = "dd/MM/yyyy")]
        public DateTime PaymentDate { get; set; }

        [Required()]
        [Display(Name = "Order ID")]
        public Int64 OrderID { get; set; }

        [Display(Name = "Pay Amount")]
        public decimal PayAmount { get; set; }


        [Display(Name = "Payment Terms")]
        public string PaymentTerms { get; set; }

        

        [Display(Name = "Slip No")]
        public string SlipNo { get; set; }

    }
}