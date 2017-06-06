using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlackSys.Models.ViewModels
{
    public class AppointmentViewModel
    {

        public Appointment Appointments { get; set; }
        public List<AppointmentListViewModel> AppointmentList { get; set; }

        public List<OrderDetails> OrderDetailsList { get; set; }

        public ServicePayment ServicePayment { get; set; }
        
       
    }
}