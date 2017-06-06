using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blacksys.Controllers;
using System.Web.Mvc;


namespace BlackSys.Models
{
    public class Employee : UserIdentification
    {

        [Key]
        public Int64 EmpID { get; set; }        

        //[Required()]
        //[Display(Name = "Organization", Prompt = "Organization")]
        //public int OrgID { get; set; }

        //[Required()]
        [Display(Name = "Branch", Prompt = "Branch")]
        
        public int BranchID { get; set; }

        //[Required()]
        [Display(Name = "Employee Id", Prompt = "Employee Id")]
        [MaxLength(10, ErrorMessage = "The Employee Id field cannot be longer than 10 characters.")]
        public string EmployeeId { get; set; }

        [UIHint("Int")]
        //[Required()]
        [Display(Name = "ID in Machine", Prompt = "ID in Machine")]
        [DisplayFormat(DataFormatString = "{0:###}")]
        //[Range(0, 10)]
        //[RegularExpression("([1-9][0-9]*)", ErrorMessage = "The ID in Machine field is not valid")]
        public int IDInMachine { get; set; }

        //[Required()]
        [Display(Name = "Employee Name", Prompt = "Employee Name")]
        [MaxLength(100, ErrorMessage = "The Employee Name field cannot be longer than 100 characters.")]
        public string EmpName { get; set; }

        //[Required()]
        [DataType(DataType.Text)]
        [Display(Name = "Address", Prompt = "Address")]
        [MaxLength(500, ErrorMessage = "The Employee Address field cannot be longer than 500 characters.")]
        public string ResAddress { get; set; }

        
        //[Required()]
        //[DataType(DataType.MultilineText)]
        //[Display(Name = "Employee Address", Prompt = "Employee Address")]
        //[MaxLength(500, ErrorMessage = "The Employee Address field cannot be longer than 500 characters.")]
        //public string EmpAddress { get; set; }

        //[Required()]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Mobile No", Prompt = "Mobile No")]
        [MaxLength(100, ErrorMessage = "Mobile No cannot be longer than 100 characters.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "The Mobile No field is not valid")]
        public string EmpMobileNo { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", Prompt = "Email")]
        [MaxLength(100, ErrorMessage = "The Email cannot be longer than {1} characters.")]
        public string EmpEmail { get; set; }
      
      


        [Required()]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Joining Date", Prompt = "dd/MM/yyyy")]
        public DateTime JoiningDate { get; set; }

        [Required]
        [Display(Name = "Gross Salary", Prompt = "Gross Salary")]
        public decimal GrossSalary { get; set; }


        [Required()]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth Date", Prompt = "dd/MM/yyyy")]
        public DateTime BirthDate { get; set; }


        [Display(Name = "Fathers Name", Prompt = "FathersName")]
        public string FathersName { get; set; }

        [Display(Name = "Mothers Name", Prompt = "MothersName")]
        public string MothersName { get; set; }

        
        [Display(Name = "Religion", Prompt = "Religion")]
        public Religionlist Religion { get; set; }

        [Display(Name = "Marital Status", Prompt = "Marital Status")]
        public MaritalStatuslist MaritalStatus { get; set; }

        [Display(Name = "Nationality", Prompt = "Nationality")]
        public string Nationality { get; set; }


        [Display(Name = "National ID No.", Prompt = "National ID No.")]
        public string NationalIDNo { get; set; }

        [Display(Name = "Sex", Prompt = "Sex")]
        public Sexlist Sex { get; set; }

        [Display(Name = "Blood Group", Prompt = "Blood Group")]
        public BloodGrouplist BloodGroup { get; set; }


        [Required()]
        [Display(Name = "Department", Prompt = "Department")]
        public int DeptID { get; set; }

        [Required()]
        [Display(Name = "Designation", Prompt = "Designation")]
        public int DesignationtID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Resignation Date", Prompt = "dd/MM/yyyy")]       
        public DateTime? ResignationDate { get; set; }

        [NotMapped]
        public string DepName { get; set;}
        [NotMapped]
        public string BranchName {get; set;}
        public virtual ICollection<File> Files { get; set; }
        
        //---------------------------------------------------------------

        //[NotMapped]
        //[Display(Name = "Organization", Prompt = "Organization")]
        //public string OrgName { get; set; }

        //[NotMapped]
        //[Display(Name = "Branch", Prompt = "Branch")]
        //public string BranchName { get; set; }

        //[NotMapped]
        //public string DeptName { get; set; }

    }

    
    public enum Sexlist
    {

        Male = 1,
        Female = 2
    }

    public enum MaritalStatuslist
    {

        Single = 1,
        Married = 2,
        Widow = 3,
        Divorced = 4,
        Separated = 5
    }

    public enum Religionlist
    {

        Islam = 1,
        Buddist = 2,
        Christian = 3,
        Hindu = 4,
        Others = 5
    }

    public enum BloodGrouplist
    {

        ABPositive = 1,
        ABNegative = 2,
        OPositive = 3,
        ONegative = 4,
        APositive = 5,
        ANegative = 6,
        BPositive = 7,
        BNegative = 8
    }
}