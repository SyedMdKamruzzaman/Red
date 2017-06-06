
using Blacksys.Controllers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace BlackSys.Models
{
    public class Template : UserIdentification
    {
        [Key]
        public int Id { get; set; }

        [Required()]
        [Display(Name = "Single Line Text", Prompt = "Single Line Text")]
        [MaxLength(10, ErrorMessage = "The {0} field cannot be longer than {1} characters.")]
        public string SinglelineText { get; set; }

        [Required()]
        [DataType(DataType.Password)]
        [Display(Name = "Password", Prompt = "Password")]
        public string Password { get; set; }

        [Required()]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Multiline Text", Prompt = "Multiline Text")]
        [MaxLength(500, ErrorMessage = "The {0} field cannot be longer than {1} characters.")]
        public string MultilineText { get; set; }

        [UIHint("Int")]
        [Required()]
        [Display(Name = "Integer", Prompt = "Integer")]
        [DisplayFormat(DataFormatString = "{0:###}")]
        //[Range(0, 10)]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "The {0} field is not valid")]
        public int Integer { get; set; }

        [Required()]
        [Display(Name = "Double", Prompt = "Double")]
        [DisplayFormat(DataFormatString = "{0:###.00}")]
        //[Range(0, 10)]
        //[RegularExpression("([1-9][0-9]*)", ErrorMessage = "Shift Value must be a non negative integer")]
        public double Double { get; set; }

        [Required()]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Mobile No", Prompt = "Mobile No")]
        [MaxLength(100, ErrorMessage = "The {0} field cannot be longer than {1} characters.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "The {0} field is not valid")]
        public string MobileNo { get; set; }

        //[Required()]        
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", Prompt = "Email")]
        [MaxLength(100, ErrorMessage = "The {0} field cannot be longer than {1} characters.")]
        public string Email { get; set; }

        [Required()]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:T}", ApplyFormatInEditMode = true)]
        [Display(Name = "Time", Prompt = "HH:mm:ss")]
        [RegularExpression("^(?:(?:([01]?[0-9]|2[0-3]):)?([0-5]?[0-9]):)?([0-5]?[0-9])$", ErrorMessage = "The {0} field must be a non negative integer")]
        public DateTime Time { get; set; }       

        [Required()]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date", Prompt = "dd/mm/yyyy")]
        public DateTime Date { get; set; }

        [UIHint("Image")]
        [ScaffoldColumnAttribute(false)]
        public byte[] Logo { get; set; }

        [NotMapped]
        [Required()]
        //[DataType(DataType.Upload)]
        [Display(Name = "Upload", Prompt = "Upload")]
        public HttpPostedFileBase Upload { get; set; }

        //[Required()]
        //[DataType(DataType.)]
        [Display(Name = "Is Aclive?", Prompt = "Is Aclive?")]
        public bool Active { get; set; }

        //List<SelectListItem> list_Branch = new List<SelectListItem>();
        //ViewBag.Branch = new SelectList(list_Branch, "Value", "Text");

        //ViewBag.Partner = new SelectListItem[] { 
        //        new SelectListItem() { Value = "", Text = "All", Selected = true },
        //        new SelectListItem() { Value = "Ericsson", Text = "Ericsson" },
        //        new SelectListItem() { Value = "Huawei", Text = "Huawei" }
        //    };



    }
}