using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlackSys.Models
{
    public class SpecialDiscount
    {
        [Key]
        public int Id { get; set; }

        public string DiscountName { get; set; }

        public decimal AllowSpecialDiscount { get; set;}

        public decimal DiscountValue { get; set; }

        public bool IsHappyHour { get; set; }

    }
}