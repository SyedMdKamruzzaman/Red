using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blacksys.Controllers;

namespace BlackSys.Models
{
    public class Vendor:UserIdentification
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Company Name", Prompt = "Company Name")]
        public string CompanyName { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Company Address", Prompt = "Company Address")]
        public string CompanyAddress { get; set; }

        [Required]
        [Display(Name = "Contact Person", Prompt = "Contact Person")]
        public string ContactPerson { get; set; }
        
        [Display(Name = "Telephone", Prompt = "Telephone")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        [Required]
        [Display(Name = "Mobile", Prompt = "Mobile")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        
        [Display(Name = "Email", Prompt = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required()]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Vendor Create Date", Prompt = "dd/MM/yyyy")]
        public DateTime VendorInDate { get; set; }

        [Required()]
        public bool IsActive { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Vendor Left Date", Prompt = "dd/MM/yyyy")]
        public DateTime VendorOutDate { get; set; }

    }
}