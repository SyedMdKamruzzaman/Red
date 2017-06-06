
using Blacksys.Controllers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlackSys.Models
{
    public class Holiday : UserIdentification
    {
        [Key]
        public int LeaveId { get; set; }

        

        [Required()]
        [Display(Name = "Branch", Prompt = "Branch")]
        public int BranchID { get; set; }

        [Required()]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Holiday Date", Prompt = "dd/mm/yyyy")]
        public DateTime HolidayDate { get; set; }

        [Required()]
        [Display(Name = "Description", Prompt = "Description")]
        [MaxLength(100, ErrorMessage = "The Description field cannot be longer then 100 characters.")]
        public string Description { get; set; }

        [Display(Name = "Is Active?", Prompt = "Is Active?")]
        public bool Active { get; set; }

        //---------------------------------------------------------------       

        [NotMapped]
        [Display(Name = "Branch", Prompt = "Branch")]
        public string BranchName { get; set; }
    }
}