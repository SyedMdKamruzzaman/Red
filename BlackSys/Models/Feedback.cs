using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlackSys.Models
{
    public class Feedback
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public Int64 OrderID { get; set; }

        [Required]
        public int QuestionID { get; set; }

        [Required]
        public int BeauticianID { get; set; }
        [Required]
        public int RatingID { get; set; }

        [DataType(DataType.MultilineText)]
        public string  Comments { get; set; }

    }
}