using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BlackSys.Models
{
    public class HappyHourModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Time", Prompt = "HH:mm:ss")]
        [RegularExpression("^(?:(?:([01]?[0-9]|2[0-3]):)?([0-5]?[0-9]):)?([0-5]?[0-9])$", ErrorMessage = "The Start Time field is not valid")]
        public string StartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Time", Prompt = "HH:mm:ss")]
        [RegularExpression("^(?:(?:([01]?[0-9]|2[0-3]):)?([0-5]?[0-9]):)?([0-5]?[0-9])$", ErrorMessage = "The End Time field is not valid")]
        public string EndTime { get; set; }

        [Display(Name = "Status", Prompt = "Status")]
        public bool IsActive { get; set; }

        public int SpecialDiscountId { get; set;}
    }
}