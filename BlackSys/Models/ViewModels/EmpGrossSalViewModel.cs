using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlackSys.Models.ViewModels
{
    public class EmpGrossSalViewModel
    {

        public bool selected { get; set; }

        [Display(Name = "Employee Id", Prompt = "Employee Id")]
        [MaxLength(10, ErrorMessage = "The Employee Id field cannot be longer than 10 characters.")]
        public string EmployeeId { get; set; }


        [Display(Name = "Employee Name", Prompt = "Employee Name")]
        [MaxLength(100, ErrorMessage = "The Employee Name field cannot be longer than 100 characters.")]
        public string EmployeeName { get; set; }


        [Required]
        [Display(Name = "Gross Salary", Prompt = "Gross Salary")]
        public decimal GrossSalary { get; set; }

        public List<EmpGrossSalViewModel> EmpGrossSalListViewModel { get; set; }
    }

    

   
}