using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BlackSys.Models
{
    public class FeedbackQuest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Questions { get; set; }

       
        public int? SelectedAnswer { get; set; } // for binding
        public IEnumerable<FeedbackRatingPoints> PossibleAnswers { get; set; }

    }
}