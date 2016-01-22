using Anketa.Models.AnswerModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Anketa.Models
{
    public class Answer
    {
        [Index(IsUnique = true)]
        public int answerID { get; set; }
        public int questionID { get; set; }
        [DisplayName("Answer text")]
        public string answerText { get; set; }
        [DisplayName("Minimum value")]
        public int minAnswerValue { get; set; }
        [DisplayName("Maximum value")]
        public int maxAnswerValue { get; set; }
        [DisplayName("Multiple choice")]
        public virtual ICollection<AnswerChoiceMultiple> selectAnswers { get; set; }
        [DisplayName("Single choice")]
        public virtual ICollection<AnswerChoiceSingle> radioAnswers { get; set; }
        public bool correct { get; set; }
    }
}