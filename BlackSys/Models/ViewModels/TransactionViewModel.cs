using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Blacksys.Controllers;
namespace BlackSys.Models.ViewModels
{
    public class TransactionViewModel
    {
        public int ID { get; set; }

   
        [Display(Name = "Category", Prompt = "Category")]
        public int CategoryID { get; set; }

  
        [Display(Name = "Accounts Name", Prompt = "Accounts Name")]
        public string AccountsName { get; set; }


      
        [Display(Name = "Amount", Prompt = "Amount")]
        public string Amount { get; set; }

 
        public DateTime Date { get; set; }

 
        [Display(Name = "Remarks", Prompt = "Remarks")]
        public string Remarks { get; set; }

        public int AppointmentID { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}