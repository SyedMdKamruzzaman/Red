using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blacksys.Controllers;


namespace BlackSys.Models
{
    public class FixedAllowance : UserIdentification
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [Index("UIndex_EmployeeId", 1, IsUnique = true)]
        [MaxLength(500, ErrorMessage = "Employee Id cannot be longer then 500 characters.")]
        public string EmployeeId { get; set; }


        [Required]
        [Index("UIndex_EmployeeId", 2, IsUnique = true)]
        public int AllowanceId { get; set; }

        [NotMapped]
        [Display(Name = "Allowance Name", Prompt = "Allowance Name")]
        public string AllowanceName { get; set; }
        public decimal AllowanceAmount { get; set; }

        public string EmployeeBankAccount { get; set; }

        public string SalaryBankAccount { get; set; }

        public bool IsActive { get; set; }

        [NotMapped]
        [Display(Name="Status",Prompt="Status")]
        public string ActiveStatus { get; set; }

    }



}