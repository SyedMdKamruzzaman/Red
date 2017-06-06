using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlackSys.Models.ViewModels
{
    public class BranchViewModel
    {
        public Branch Branches { get; set; }
        public List<Branch> BrachList { get; set; }
    }
}