using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Blacksys.Controllers;

namespace BlackSys.Models
{
    public class TargetVsAch : UserIdentification
    {
        [Key]
        public int ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Target Date", Prompt = "dd/MM/yyyy")]
        public DateTime TargetDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expire Date", Prompt = "dd/MM/yyyy")]
        public DateTime ExpireDate { get; set; }

        [NotMapped]
        public string TargetMonthInText { get; set; }

        public int BranchId { get; set; }

        public Int64 AssignTo { get; set; }
        [NotMapped]
        public string SPName { get; set; }

        [NotMapped]
        public string BranchName { get; set; }

        public decimal TargetAmount { get; set; }

        public decimal AchAmount { get; set; }
        public decimal GapAmount { get; set; }

        [NotMapped]
        public string DaysRemain { get; set; }

        public bool IsDone { get; set; }


    }
}