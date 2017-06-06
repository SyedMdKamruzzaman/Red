using Blacksys.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlackSys.Models
{
    public class CategoryModel : UserIdentification
    {
        [Key]
        public int categoryid { get; set; }

        [Required]
        [Display(Name = "Category Name", Prompt = "Category Name")]
        [MaxLength(100, ErrorMessage = "The Category Name field cannot be longer then 100 characters.")]
        public string category { get; set; }

    }
}