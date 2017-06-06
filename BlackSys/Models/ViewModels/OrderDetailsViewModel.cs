using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlackSys.Models.ViewModels
{
    public class OrderDetailsViewModel
    {
        public List<OrderViewModel> OrderDetailsList { get; set; }

        public List<BeauticianViewModel> BeauticianList { get; set; }
    }
}