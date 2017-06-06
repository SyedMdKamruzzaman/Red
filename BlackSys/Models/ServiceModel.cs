

namespace BlackSys.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using Blacksys.Controllers;
    using System.ComponentModel;
    using System.Web;
    using System.Net;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    [Table("Services")]
    public partial class Service : UserIdentification
    {
       
        [Key]
        public int ID { get; set; }

        [Required()]
        public int ServiceCategory { get; set; }


        [Required()]
        [Display(Name = "Service Name", Prompt = "Service Name")]
        public string ServiceName { get; set; }

        [Required()]
        [Display(Name = "Price", Prompt = "Price")]
        public decimal Price { get; set; }      

    }    
}