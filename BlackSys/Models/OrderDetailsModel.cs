using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using Blacksys.Controllers;
using System.ComponentModel;
using BlackSys.Models.ViewModels;

namespace BlackSys.Models
{
    
    public class OrderDetails : UserIdentification
    {
         
        
        [Key]
        public int ID { get; set; }

        public Int64 BookingID { get; set; }

        public Int64 JobID { get; set; }

        public Int64 ServiceID { get; set; }

        [NotMapped]
        [Display(Name = "Service")]
        public string Service { get; set; }

        public Decimal ServiceRate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Service Date", Prompt = "dd/MM/yyyy")]
        public DateTime ServiceDate { get; set; }

        public Nullable<Int64> SpecialSaleBeauticinId { get; set; }

        [Required()]
        [Display(Name = "Beautician", Prompt = "Beautician")]
        public Int64 Beautician { get; set; }

        
        

    }
}