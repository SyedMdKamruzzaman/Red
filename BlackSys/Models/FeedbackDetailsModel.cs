using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BlackSys.Models
{
    public class FeedbackDetails
    {
        [Key]
        public Int64 Id { get; set; }

        public Int64 OrderId { get; set; }

        public Int64 BeauticianId { get; set; }

        public int QuestionId { get; set; }

        public int RatingPoint { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comments", Prompt = "Comments")]
        public string Comments { get; set; }

    }
}