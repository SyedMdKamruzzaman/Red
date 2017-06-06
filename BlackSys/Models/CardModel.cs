using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blacksys.Controllers;


namespace BlackSys.Models
{
    public class Card: UserIdentification
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Card Id")]
        public string CardId { get; set; }

        [Display(Name = "Card Type")]
        public int CardTypeId { get; set; }

        [Display(Name = "Card  Discount (%)")]
        public decimal CardDiscount { get; set; }
    }

    
}