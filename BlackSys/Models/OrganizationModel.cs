using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blacksys.Controllers;


namespace BlackSys.Models
{
    public class Organization : UserIdentification
    {
        [Key]
        public int OrgID { get; set; }

        [Required()]        
        [Display(Name = "Organization Name", Prompt = "Organization Name")]
        [MaxLength(200, ErrorMessage = "Organization Name cannot be longer than 200 characters.")]
        public string OrgName { get; set; }

        [Required()]        
        [DataType(DataType.MultilineText)]
        [Display(Name = "Organization Address", Prompt = "Organization Address")]
        [MaxLength(500, ErrorMessage = "Organization Address cannot be longer than 500 characters.")]
        public string OrgAddress { get; set; }

        [Required()]        
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Mobile No", Prompt = "Mobile No")]
        [MaxLength(100, ErrorMessage = "Mobile No cannot be longer than 100 characters.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "The Mobile No field is not valid")]
        public string MobileNo { get; set; }

        //[Required()]
        //[DataType(DataType.)]
        [Display(Name = "Fax No", Prompt = "Fax No")]
        [MaxLength(100, ErrorMessage = "Fax No cannot be longer than 100 characters.")]
        public string FaxNo { get; set; }

        //[Required()]        
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", Prompt = "Email")]
        [MaxLength(100, ErrorMessage = "The Email field cannot be longer than 100 characters.")]
        public string Email { get; set; }

        //[Required()]
        //[DataType(DataType.)]
        [Display(Name = "Is Aclive?", Prompt = "Is Aclive?")]
        public bool Active { get; set; }

        [UIHint("Image")]
        [ScaffoldColumnAttribute(false)]
        public byte[] Logo { get; set; }

        [NotMapped]
        //[Required()]
        //[DataType(DataType.Upload)]
        [Display(Name = "Upload Logo", Prompt = "Select Logo")]
        public HttpPostedFileBase UploadLogo { get; set; }
    }
}