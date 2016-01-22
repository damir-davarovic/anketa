using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Anketa.Models.AnswerModels
{
    public class AnswerChoiceMultiple
    {
        [Key, Index]
        public int choiceId { get; set; }
        public int answerID { get; set; }
        public int orderNo { get; set; }
        public string choiceText { get; set; }
    }
}