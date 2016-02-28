using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Anketa.Models.SurveySolvedModels
{
    public class AnswerChosen
    {
        public int answerChosenID { get; set; }
        public int solvingID { get; set; }
        public int questionID { get; set; }
        public int questionType { get; set; }
        [Required(ErrorMessage = "Answer is required")]
        public string answerText { get; set; }
        public int chosenScaleValue { get; set; }

        [DisplayName("Chosen Multiple choice")]
        public virtual ICollection<ChosenMultipleChoice> selectAnswers { get; set; }
        [DisplayName("Chosen single choice")]
        public virtual ICollection<ChosenSingleChoice> radioAnswers { get; set; }
    }
}
