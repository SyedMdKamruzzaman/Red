
using Blacksys.Controllers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlackSys.Models
{
    public class Shift : UserIdentification
    {
        [Key]
        public int ShiftId { get; set; }

       

        [Required()]
        [Display(Name = "Branch", Prompt = "Branch")]
        public int BranchID { get; set; }

        [Required()]
        [Display(Name = "Shift Code", Prompt = "Shift Code")]
        [MaxLength(10, ErrorMessage = "Shift Code cannot be longer then 10 characters.")]
        public string ShiftCode { get; set; }
        
        [Required()]
        [Display(Name = "Shift Name", Prompt = "Shift Name")]
        [MaxLength(50, ErrorMessage = "Shift Code cannot be longer then 50 characters.")]
        public string ShiftName { get; set; }

        [Required()]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Shift In Time", Prompt = "HH:mm:ss")]
        [RegularExpression("^(?:(?:([01]?[0-9]|2[0-3]):)?([0-5]?[0-9]):)?([0-5]?[0-9])$", ErrorMessage = "The Shift In Time field is not valid")]
        public string ShiftInTime { get; set; }

        [UIHint("Int")]
        [Required()]
        [Display(Name = "Mercy In Time (Min.)", Prompt = "Mercy In Time (Min.)")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        //[Range(0, 10)]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "The {0} field is not valid")]
        public int MercyTimeIn { get; set; }

        [Required()]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Shift Out Time", Prompt = "HH:mm:ss")]
        [RegularExpression("^(?:(?:([01]?[0-9]|2[0-3]):)?([0-5]?[0-9]):)?([0-5]?[0-9])$", ErrorMessage = "The Shift Out Time field is not valid")]
        public string ShiftOutTime { get; set; }

        [UIHint("Int")]
        [Required()]
        [Display(Name = "Mercy Out Time (Min.)", Prompt = "Mercy Out Time (Min.)")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        //[Range(0, 10)]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "The {0} field is not valid")]
        public int MercyTimeOut { get; set; }

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