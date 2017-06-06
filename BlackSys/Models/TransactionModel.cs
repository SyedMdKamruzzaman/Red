using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Blacksys.Controllers;
using System.ComponentModel.DataAnnotations.Schema;



namespace BlackSys.Models
{
    public class Transaction : UserIdentification
    {
        [Key]
        public Int64 ID { get; set; }

        [Required]
        [Display(Name = "Category ID", Prompt = "Category")]
        public int CategoryID { get; set; }

        [NotMapped]
        [Display(Name = "Category", Prompt = "Category")]
        public string Category { get; set; }

        [Required]
        [Display(Name = "Sub Category", Prompt = "Sub Category")]
        public int SubCategoryID { get; set; }

        [NotMapped]
        [Display(Name = "Sub Category", Prompt = "Sub Category")]
        public string SubCategory { get; set; }

        [Required]
        [Display(Name = "Accounts Name", Prompt = "Accounts Name")]
        public string AccountsName { get; set; }


        [Required]
        [Display(Name = "Amount", Prompt = "Amount")]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date", Prompt = "dd/mm/yyyy")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Remarks", Prompt = "Remarks")]
        public string Remarks { get; set; }


      
        public int AppointmentID { get; set; }

      

        [Required]
        [Display(Name = "Branch", Prompt = "Branch")]
        public int BranchId { get; set; }


        [Required]
        [Display(Name = "Credit Debit", Prompt = "Credit Debit")]
        [StringLength(1, ErrorMessage = "Max length 1 characters")]
        public string CreditDebitFlag { get; set; }

        [NotMapped]
        public string CreditDebit { get; set; }

        public Transaction()
        {
            AppointmentID = 0;
        }

    }

    //public enum Categories
    //{
    //    Asset = 0,
    //    Income = 1,
    //    [Description("Liability/Credit Card")]
    //    Liability = 2,
    //    Expense = 3,
    //    Equity = 4
    //}
}