using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlackSys.Models
{
    public class Education
    {
        [Key]
        public int ID { get; set; }


           [Display(Name = "Employee ID", Prompt = "Employee ID")]
        public int EmployeeID { get; set; }

        [Display(Name = "Level of Education", Prompt = "Level of Education")]
        public LevelofEducation LevelofEducation { get; set; }

        [Display(Name = "College/ University/ Institution/ Board", Prompt = "College/ University/ Institution/ Board")]
        public string Institution { get; set; }

        [Display(Name = "Roll No", Prompt = "Roll No")]
        public string RollNo { get; set; }

        [Display(Name = "Class/ CGPA", Prompt = "Class/ CGPA")]
        public string ClassCGPA { get; set; }

        [Display(Name = "Year", Prompt = "Year")]
        public string Year { get; set; }

        [Display(Name = "Duration", Prompt = "Duration")]
        public string Duration { get; set; }

        [Display(Name = "Country", Prompt = "Country")]
        public string Country { get; set; }
    }

    public enum LevelofEducation
    {
        [Display(Name="SSC/ Equivalent")]
        SSC = 1,

        [Display(Name = "HSC/ Equivalent")]
        HSC = 2,

        [Display(Name = "Bachelor's Degree")]
        BachelorsDegree = 3,

        [Display(Name = "Masters")]
        Masters = 4,

        [Display(Name = "Doctorate")]
        Doctorate = 5


    }
}