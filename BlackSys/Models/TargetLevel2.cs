using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Blacksys.Controllers;

namespace BlackSys.Models
{

        public class TargetLevel2 : UserIdentification
        {
        [Key]
        public int TargetLevel2Id { get; set; }
        [Required]
        public int BranchId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Target Date", Prompt = "dd/MM/yyyy")]
        public DateTime TargetDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expire Date", Prompt = "dd/MM/yyyy")]
        public DateTime ExpireDate { get; set; }
        [Required]
        public decimal TargetAmount { get; set; }
        public string NumberOfDays { get; set; }
        [Required]
        public Int64 AssignBy { get; set; }
        [Required]
        public Int64 AssignTo { get; set; }
        [Required]
        public bool IsActive { get; set; }


        [NotMapped]
        [Display(Name = "Branch Name", Prompt = "Branch Name")]
        public string BranchName { get; set; }

        [NotMapped]
        [Display(Name = "Employee Name", Prompt = "Employee Name")]
        public string EmployeeName { get; set; }

        [NotMapped]
        [Display(Name = "Supervisor Name", Prompt = "Supervisor Name")]
        public string SupervisorName { get; set; }

        [NotMapped]
        [Display(Name = "Target Month", Prompt = "Target Month")]
        public string TargetMonth { get; set; }

        [NotMapped]
        [Display(Name = "Days Remain", Prompt = "Days Remain")]
        public string DaysRemain { get; set; }

        [NotMapped]
        [Display(Name = "Total Days", Prompt = "Total Days")]
        public string TotalDays { get; set; }

        [NotMapped]
        [Display(Name = "Acheivement", Prompt = "Acheivement")]
        public string TotalAcheivement { get; set; }

    }
}