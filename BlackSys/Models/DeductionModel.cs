using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blacksys.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlackSys.Models
{
    public class Deduction : UserIdentification
    {
        [Key]       
        public int Id { get; set; }

        [Required]
        [Display(Name = "Deduction Name", Prompt = "Deduction Name")]
        public string DeductionName { get; set; }

        [Required]
        [Display(Name = "Deduction Category", Prompt = "Deduction Category")]
        public int DeductionCategoryId { get; set; }

        [NotMapped]
        public string DeductionCategory { get; set; }

        [Required]
        [Display(Name = "Deduction Type", Prompt = "Deduction Type")]
        public int DeductionTypeId { get; set; }

        [NotMapped]
        public string DeductionType { get; set; }

        [Display(Name = "Is Manage By System", Prompt = "Is Manage By System")]
        public bool IsManageBySystem { get; set; }


        [NotMapped]
        [Display(Name = "Manage by System", Prompt = "Manage by System")]
        public string ManagebySystem { get; set; }

        [Display(Name = "Is Part of Gross Salary", Prompt = "Is Part of Gross Salary")]
        public bool IsPartOfGrossSalary { get; set; }

        [NotMapped]
        [Display(Name = "Part of Gross Salary", Prompt = "Part of Gross Salary")]
        public string PartofGrossSalary { get; set; }
    }
}