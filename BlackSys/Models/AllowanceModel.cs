using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blacksys.Controllers;

namespace BlackSys.Models
{
    public class Allowance: UserIdentification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name="Allowance Name",Prompt="Allowance Name")]
        public string AllowanceName { get; set; }

        [Required]
        [Display(Name="Allowance Category",Prompt="Allowance Category")]
        public int AllowanceCategoryId { get; set; }

        [NotMapped]
        public string AllowanceCategory { get; set; }
        
        [Required]
        [Display(Name="Allowance Type",Prompt="Allowance Type")]
        public int AllowanceTypeId { get; set; }

        [NotMapped]
        public string AllowanceType { get; set; }

        [Display(Name="Is Manage By System",Prompt="Is Manage By System")]
        public bool IsManageBySystem { get; set; }


        [NotMapped]
        [Display(Name = "Manage by System", Prompt = "Manage by System")]
        public string ManagebySystem { get; set; }

        [Display(Name="Is Part of Gross Salary",Prompt="Is Part of Gross Salary")]
        public bool IsPartOfGrossSalary { get; set; }

        [NotMapped]
        [Display(Name="Part of Gross Salary",Prompt="Part of Gross Salary")]
        public string PartofGrossSalary { get; set; }
    }
}