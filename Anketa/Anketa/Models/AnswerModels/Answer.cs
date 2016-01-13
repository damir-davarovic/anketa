using Anketa.Models.AnswerModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Anketa.Models
{
    public class Answer
    {
        [Index(IsUnique = true)]
        public int answerID { get; set; }
        public int QuestionID { get; set; }
        [System.ComponentModel.DefaultValue("This is what an answer should look like.")]
        public string answerText { get; set; }
        public virtual ICollection<AnswerChoiceMultiple> selectAnswers { get; set; }
        public virtual ICollection<AnswerChoiceSingle> radioAnswers { get; set; }
        public bool correct { get; set; }
    }
}