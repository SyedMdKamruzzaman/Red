using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blacksys.Controllers;



namespace BlackSys.Models
{
    public class FixedDeduction: UserIdentification
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [Index("UIndex_EmployeeId", 1, IsUnique = true)]
        [MaxLength(500, ErrorMessage = "Employee Id cannot be longer then 500 characters.")]
        public string EmployeeId { get; set; }


        [Required]
        [Index("UIndex_EmployeeId", 2, IsUnique = true)]
        public int DeductionId { get; set; }

        [NotMapped]
        [Display(Name = "Deduction Name", Prompt = "Deduction Name")]
        public string DeductionName { get; set; }
        public decimal DeductionAmount { get; set; }

        public bool IsActive { get; set; }

        [NotMapped]
        [Display(Name = "Status", Prompt = "Status")]
        public string ActiveStatus { get; set; }
    
    }

}