using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using  System.ComponentModel.DataAnnotations;

namespace BlackSys.Models.ViewModels
{
    public class OrderViewModel
    {
        public int ID { get; set; }

        public Int64 BookingID { get; set; }

        public Int64 JobID { get; set; }

        public Int64 ServiceID { get; set; }

        public Decimal ServiceRate { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Service Date", Prompt = "dd/MM/yyyy")]
        public DateTime ServiceDate { get; set; }

        public Int64 Beautician { get; set; }

        public string ServiceName { get; set; }

        public Nullable<Int64> SpecialSaleBeauticinId { get; set; }
    }
}