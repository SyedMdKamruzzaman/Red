using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blacksys.Controllers;

namespace BlackSys.Models
{
    public class AccountsHeadCategory
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string category { get; set; }
    }
}