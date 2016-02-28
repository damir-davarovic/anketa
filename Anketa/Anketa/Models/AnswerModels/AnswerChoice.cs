using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Anketa.Models.AnswerModels
{
    public abstract class AnswerChoice
    {
        //https://msdn.microsoft.com/en-us/data/jj591617#2.6
        [Key, Index]
        public int choiceId { get; set; }
        public int answerID { get; set; }
        public int orderNo { get; set; }
        public string choiceText { get; set; }
    }
}