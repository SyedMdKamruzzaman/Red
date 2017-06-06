using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blacksys.Controllers;
using System.Web.Mvc;

namespace BlackSys.Models
{
    public class ProductConditionModel
    {
        [Key]
        public int ID { get; set; }
        public string ConditionName { get; set; }
    }
}