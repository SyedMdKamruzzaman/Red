using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blacksys.Controllers;
using System.ComponentModel.DataAnnotations;

namespace BlackSys.Models
{
    public class CardType:UserIdentification
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Card Type",Prompt ="Card Type")]
        public string Type { get; set; }

        public Decimal DiscountPercentage { get; set; }
    }
}