using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blacksys.Controllers;
using System.ComponentModel;

namespace BlackSys.Models
{
    public class MembershipInfo: UserIdentification
    {
        public MembershipInfo()
        {
            RegistrationDate = DateTime.Now;
            AnniversaryDate = DateTime.Now;
            BirthDate = DateTime.Now;
            MemberSourceId = 0;            
        }

        [Key]
        public int ID { get; set; }

        [Display(Name = "MemberShipCard Id")]
        public string CardId { get; set; }

        [NotMapped]
        public string CardType { get; set; }

        [Display(Name = "Card Type")]
        public int? CardTypeId { get; set; }

        [Display(Name = "Card  Discount (%)")]
        public decimal? Discount { get; set; }

        
        [Display(Name = "Total Point")]
        public decimal? TotalPointAchieved { get; set; }

        [Required]
        [Display(Name = "Member Full Name")]
        public string MemberName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Mobile No", Prompt = "Mobile No")]
        [MaxLength(11, ErrorMessage = "Mobile No cannot be longer than 11 characters.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "The Mobile No field is not valid")]
        public string MemMobileNo { get; set; }

        //[Required]
        //[DataType(DataType.EmailAddress)]
        [Display(Name = "Email", Prompt = "Email")]
        //[MaxLength(100, ErrorMessage = "The Email cannot be longer than {1} characters.")]
        public string MemEmail { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Registration Date", Prompt = "dd-MMM-yyyy")]
        public Nullable<DateTime> RegistrationDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth Date", Prompt = "dd-MMM-yyyy")]
        public Nullable<DateTime> BirthDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Anniversary Date", Prompt = "dd-MMM-yyyy")]
        public Nullable<DateTime> AnniversaryDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        public int BranchId { get; set; }
  
        [Required]
        [Display(Name = "Member Source", Prompt = "Member Source")]
        public Nullable<int> MemberSourceId { get; set; }

        [NotMapped]
        public string MemberSource { get; set; }

        [NotMapped]
        public string BranchName { get;  set; }
    }


    
}