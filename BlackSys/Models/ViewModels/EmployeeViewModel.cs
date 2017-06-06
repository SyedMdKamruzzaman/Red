using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlackSys.Models
{
    public class EmployeeViewModel
    {
        public Employee Employees { get; set; }
        public List<Employee> EmployeeList { get; set; }
    }
}