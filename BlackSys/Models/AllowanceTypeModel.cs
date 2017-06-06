using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlackSys.Models
{
    public class AllowanceType
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [Display(Name = "Allowance Type", Prompt = "Allowance Type")]
        public string Type { get; set; }
    }
}