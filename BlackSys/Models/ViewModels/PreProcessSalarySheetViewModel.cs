using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlackSys.Models.ViewModels
{
    public class PreProcessSalarySheetViewModel
    {
        [Key]                
        public int ID { get; set; }
        public List<FixedAllowance> FixedAllowanceList { get; set; }
        public List<VariableAllowance> VariableAllowanceList { get; set; }
        public List<FixedDeduction> FixedDeductionList { get; set; }
        public List<VariableDeduction> VariableDeductionList { get; set; }

    }
}