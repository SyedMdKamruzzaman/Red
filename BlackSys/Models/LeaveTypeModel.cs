
using Blacksys.Controllers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlackSys.Models
{
    public class LeaveType : UserIdentification
    {
        [Key]
        public int LeaveTypeId { get; set; }

        

        [Required()]
        [Display(Name = "Branch", Prompt = "Branch")]
        public int BranchID { get; set; }

        [Required()]
        [Display(Name = "Short Code", Prompt = "Short Code")]
        [MaxLength(10, ErrorMessage = "The {0} field cannot be longer then {1} characters.")]
        public string ShortCode { get; set; }

        [Required()]
        [Display(Name = "Description", Prompt = "Description")]
        [MaxLength(100, ErrorMessage = "The {0} field cannot be longer then {1} characters.")]
        public string Description { get; set; }

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

    }
}