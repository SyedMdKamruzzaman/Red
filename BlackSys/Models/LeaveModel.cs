
using Blacksys.Controllers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlackSys.Models
{
    public class Leave : UserIdentification
    {
        [Key]
        public int LeaveId { get; set; }

        

        [Required()]
        [Display(Name = "Branch", Prompt = "Branch")]
        public int BranchID { get; set; }

        [Required()]
        [Display(Name = "Employee", Prompt = "Employee")]
        public int EmployeeID { get; set; }

        [Required()]
        [Display(Name = "Leave Type", Prompt = "Leave Type")]
        [MaxLength(10)]
        public string LeaveType { get; set; }

        [UIHint("Int")]
        [Required()]
        [Display(Name = "Total Days", Prompt = "Total Days")]
        [DisplayFormat(DataFormatString = "{0:###}")]
        //[Range(0, 10)]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "The Total Days field is not valid")]
        public int TotalDays { get; set; }

        [Required()]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date From", Prompt = "dd/mm/yyyy")]
        public DateTime FromDate { get; set; }

        [Required()]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date To", Prompt = "dd/mm/yyyy")]
        public DateTime ToDate { get; set; }

        //---------------------------------------------------------------


        [NotMapped]
        [Display(Name = "Branch", Prompt = "Branch")]
        public string BranchName { get; set; }

        [NotMapped]
        [Display(Name = "Employee", Prompt = "Employee")]
        public string EmployeeName { get; set; }
    }
}