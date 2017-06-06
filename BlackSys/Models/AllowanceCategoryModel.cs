using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlackSys.Models
{
    public class AllowanceCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name="Category Name",Prompt="Category Name")]
        public string Name { get; set; }

    }
}