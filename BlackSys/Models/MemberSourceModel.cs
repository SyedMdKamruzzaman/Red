using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blacksys.Controllers;
using System.ComponentModel;

namespace BlackSys.Models
{
    public class MemberSource
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}