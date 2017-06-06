using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AEAMS.Models.ViewModels
{
    public class AccessLogViewModel
    {
        [Key]
        [Required()]
        [Display(Name = "Organization", Prompt = "Organization")]
        public int OrgID { get; set; }

        [Required()]
        [Display(Name = "Branch", Prompt = "Branch")]
        public int BranchID { get; set; }

        [Display(Name = "Employee", Prompt = "Employee")]
        public int? EmployeeID { get; set; }

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

        [Display(Name = "Organization", Prompt = "Organization")]
        public string OrgName { get; set; }

        [Display(Name = "Branch", Prompt = "Branch")]
        public string BranchName { get; set; }

        public List<AccessLog> AccessLogs { get; set; }


    }
}