using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using Blacksys.Controllers;
using System.ComponentModel;

namespace BlackSys.Models
{

    public class ServicePayment : UserIdentification
    {
        [Key]
        public Int64 ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Payment Date", Prompt = "dd/MM/yyyy")]
        public DateTime PaymentDate { get; set; }

        [Required()]
        [Display(Name = "Order ID")]
        public Int64 OrderID { get; set; }

        [Display(Name = "Member Card ID")]
        public string MemberCardID { get; set; }

        [Display(Name = "Card  Type")]
        public int? CardTypeId { get; set; }

      [NotMapped]
        public string CardType { get; set; }

        [Display(Name = "Card  Discount")]
        public decimal CardDiscount { get; set; }

        [Display(Name = "Card  Discount Amount")]
        public decimal CardDiscountAmount { get; set; }

        

        [Display(Name = "Total Service Amount")]
        public decimal TotalServiceAmount { get; set; }

        [Display(Name = "Booking Payment")]
        public decimal BookingPayment { get; set; }

        [Display(Name = "Special  Discount")]
        public Nullable<decimal> SpecialDiscount { get; set; }

        [Display(Name = "Total  Discount")]
        public decimal TotalDiscount { get; set; }

        [Display(Name = "Payable Amount")]
        public decimal PayableAmount { get; set; }

        [Display(Name = "Paid Amount")]
        public decimal AdvancePayment { get; set; }

        [Display(Name = "Pay Amount")]
        public decimal PayAmount { get; set; }

        [Display(Name = "Due")]
        public decimal Due { get; set; }

        [Display(Name = "Payment Terms")]
        public int PaymentTermsId { get; set; }

        [Display(Name = "Credit Card No")]
        public Nullable<int> CreditCardNo { get; set; }



        [Display(Name = "Slip No")]
        public string SlipNo { get; set; }

        [NotMapped]
        [Display(Name = "Payment Terms")]
        public string PaymentTerms { get; set; }


        [Display(Name = "Special Discount")]
        public int? SpecialDiscountId { get; set; }

        [Display(Name = "Special Discount(%)")]
        public decimal SpecialDiscountPer { get; set; }

        [Display(Name = "Special  Discount Amount")]
        public decimal SpecialDiscountAmount { get; set; }

    }
}