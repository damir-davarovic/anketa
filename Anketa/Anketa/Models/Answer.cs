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
        public string answerText { get; set; }
        public bool correct { get; set; }
    }
}