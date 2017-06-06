using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BlackSys.Models
{
    public class PaymentTermsModel
    {
        [Key]
        public int Id { get; set; }

        public string PaymentTermsName { get; set; }
    }
}