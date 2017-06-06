using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using Blacksys.Controllers;
using System.ComponentModel;

namespace BlackSys.Models
{
    [Table("AccountsSubHead")]
    public class AccountsSubHead : UserIdentification
    {
               
        [Key]
        public int ID { get; set; }

        [Required()]
        [Display(Name = "Category", Prompt = "Category")]
        public int CategoryID { get; set; }

        [NotMapped]
        public string CategoryName { get; set; }

        [Required()]
        [Display(Name = "Sub Category", Prompt = "Sub Category")]
        public int SubCategoryID { get; set; }

        [NotMapped]
        public string SubCategoryName { get; set; }


        [Required()]
        [Display(Name = "Accounts Name", Prompt = "Accounts Name")]
        public string AccountsName { get; set; }
       
        


    }
    
}