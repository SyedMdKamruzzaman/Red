using Blacksys.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlackSys.Models
{
    public class Branch : UserIdentification
    {
        [Key]
        public int BranchId { get; set; }

        //[Required()]
        //[Display(Name = "Oragnization", Prompt = "Oragnization")]
        //public int OrgID { get; set; }

        [Required()]
        [Display(Name = "Branch Name", Prompt = "Branch Name")]
        [MaxLength(200, ErrorMessage = "Branch Name cannot be longer then 200 characters.")]
        public string BranchName { get; set; }

        [UIHint("Int")]
        [Required()]
        [Display(Name = "Branch Code", Prompt = "Branch Code")]
        [DisplayFormat(DataFormatString = "{0:###}")]
        [Range(10001, 99999)]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "The {0} field is not valid")]
        //[MaxLength(10, ErrorMessage = "Branch Code cannot be longer then 10 characters.")]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "The Branch Code field is not valid")]
        [Index("UIndex_BranchCode", IsUnique = true)]
        public int BranchCode { get; set; }

        [Required()]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Branch Address", Prompt = "Branch Address")]
        [MaxLength(500, ErrorMessage = "Branch Address cannot be longer then 500 characters.")]
        public string BranchAddress { get; set; }

        //[NotMapped]
        //[Display(Name = "Organization Name", Prompt = "Organization Address")]
        //public string OrgName { get; set; }

        
    }
}