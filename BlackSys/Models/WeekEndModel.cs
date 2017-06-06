
using Blacksys.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace BlackSys.Models
{
    public class WeekEnd : UserIdentification
    {
        [Key]
        public int WeekEndId { get; set; }

       

        [Required()]
        [Display(Name = "Branch", Prompt = "Branch")]
        public int BranchID { get; set; }

        [Required()]
        [Display(Name = "Weekend", Prompt = "Weekend")]       
        public WeekDays WeekDay { get; set; }

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

   public enum WeekDays
    {
        Sunday = 1,
        Monday = 2,
        Tuesday = 3,
        Wednesday = 4,
        Thursday = 5,
        Friday = 6,
        Saturday = 7
    }
}