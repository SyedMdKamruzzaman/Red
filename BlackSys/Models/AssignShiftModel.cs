using Blacksys.Controllers;
using BlackSys.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc.Html;

namespace BlackSys.Models
{
    public class AssignShift: UserIdentification
    {
        [Key]
        public int AssignShiftId { get; set; }

        

        [Required()]
        [Display(Name = "Branch", Prompt = "Branch")]
        public int BranchID { get; set; }

        [Required()]
        [Display(Name = "Employee", Prompt = "Employee")]
        public int EmployeeID { get; set; }

        [Required()]
        [Display(Name = "Shift Code", Prompt = "Shift Code")]
        [MaxLength(10, ErrorMessage = "The {0} field cannot be longer then {1} characters.")]
        public string ShiftCode { get; set; }

        [Required()]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Effective Date", Prompt = "dd/mm/yyyy")]
        public DateTime EffectiveDate { get; set; }

        [Required()]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expire Date", Prompt = "dd/mm/yyyy")]
        public DateTime ExpireDate { get; set; }

        //---------------------------------------------------------------

        

        [NotMapped]
        [Display(Name = "Branch", Prompt = "Branch")]
        public string BranchName { get; set; }

        [NotMapped]
        [Display(Name = "Employee", Prompt = "Employee")]
        public string EmployeeName { get; set; }

        [NotMapped]
        [Display(Name = "Shift Name", Prompt = "Shift Name")]       
        public string ShiftName { get; set; }
    
    }
}