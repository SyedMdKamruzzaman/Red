using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blacksys.Controllers;

namespace BlackSys.Models
{
    public class UnitModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}