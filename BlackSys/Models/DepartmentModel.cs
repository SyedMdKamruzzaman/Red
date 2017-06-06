
using Blacksys.Controllers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace BlackSys.Models
{
    public class Department : UserIdentification
    {
        [Key]
        public int DeptId { get; set; }

        //[Required()]
        //[Display(Name = "Organization", Prompt = "Organization")]
        //public int OrgID { get; set; }            
        //[MaxLength(10, ErrorMessage = "Branch Code cannot be longer then 10 characters.")]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "The Branch Code field is not valid")]        

        [Required()]
        [Display(Name = "Department Name", Prompt = "Department Name")]
        [MaxLength(50, ErrorMessage = "The Department Name field cannot be longer then 50 characters.")]
        public string DepartmentName { get; set; }

        [Required()]
        [Display(Name = "Department Code", Prompt = "Department Code")]
        [MaxLength(10, ErrorMessage = "The Department Code field cannot be longer then 10 characters.")]
        public string DepartmentCode { get; set; }
        
        //---------------------------------------------------------------
        //[NotMapped]
        //[Display(Name = "Organization", Prompt = "Organization")]
        //public string OrgName { get; set; }

        //[NotMapped]
        //[Display(Name = "Branch", Prompt = "Branch")]
        //public IEnumerable<SelectListItem> BranchList { get; set; }
       
    }
}