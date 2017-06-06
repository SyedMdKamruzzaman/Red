﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlackSys.Models.ViewModels
{
    public class DepartmentViewModel
    {
        

        [Required()]
        [Display(Name = "Branch", Prompt = "Branch")]
        public int BranchID { get; set; }
        

        [Display(Name = "Branch", Prompt = "Branch")]
        public string BranchName { get; set; }

        public List<Department> Departments { get; set; }


    }
}