using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blacksys.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlackSys.Models
{
    public class Salary:UserIdentification
    {
        [Key]
        public Int64 Id { get; set; }

        public string SalaryId { get; set; }

        public int SalaryYearId { get; set; }

        [NotMapped]
        public string SalaryYear { get; set; }

        public int SalaryMonthId { get; set; }

        [NotMapped]
        public string SalaryMonth { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Processed Date", Prompt = "dd-MMM-yyyy")]
        public DateTime SalaryProcessedDate { get; set; }
    }
}