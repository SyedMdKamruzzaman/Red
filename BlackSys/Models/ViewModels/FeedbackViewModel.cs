using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BlackSys.Models.ViewModels
{
    public class FeedbackViewModel
    {
        [Key]
        public int Id { get; set; }

        public Int64 OrderId { get; set; }

        public Int64 BeauticianId { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comments", Prompt = "Comments")]
        public string Comments { get; set; }
        public List<FeedbackQuest> feedbackQuestionsList { get; set; }

        public FeedbackViewModel()
        {
            feedbackQuestionsList = new List<FeedbackQuest>();
        }


    }
}