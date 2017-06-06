﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlackSys.Models.ViewModels
{
    public class WeekEndViewModel
    {
        [Required()]
        [Display(Name = "Organization", Prompt = "Organization")]
        public int OrgID { get; set; }

        [Required()]
        [Display(Name = "Branch", Prompt = "Branch")]
        public int BranchID { get; set; }

        [Display(Name = "Organization", Prompt = "Organization")]
        public string OrgName { get; set; }

        [Display(Name = "Branch", Prompt = "Branch")]
        public string BranchName { get; set; }

        public List<WeekEnd> WeekEnds { get; set; }


    }
}