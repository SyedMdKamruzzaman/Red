using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlackSys.Models
{
    public class GrossSalaryDistribution
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Index("UIndex_AllowanceId", 1, IsUnique = true)]
        public int AllowanceId { get; set; }


        [Required]
        [Display(Name="Percentage Of Gross",Prompt="Percentage of Gross")]
        public int PercentageOfGross { get; set; }


        public bool IsActive { get; set; }
    }
}