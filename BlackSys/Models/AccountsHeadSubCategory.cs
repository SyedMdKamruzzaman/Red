using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using Blacksys.Controllers;
using System.ComponentModel;

namespace BlackSys.Models
{
    public class AccountsHeadSubCategory : UserIdentification
    {

        [Key]
        public int Id { get; set; }

        [Required()]
        [Display(Name = "Category", Prompt = "Category")]
        public int CategoryID { get; set; }

        [NotMapped]
        public string CategoryName { get; set; }

        [Required()]
        [Display(Name = "Accounts Head Sub Category Name", Prompt = "Accounts Head Sub Category Name")]
        public string SubCategoryName { get; set; }       


    }

    // public enum Category{
    //    Asset=0,
    //    Income=1,
    //    [Description("Liability/Credit Card")] Liability=2,
    //     Expense=3,
    //     Equity=4
    //}




}