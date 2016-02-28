using Anketa.Models.AnswerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anketa.Models.SurveySolvedModels
{
    public class ChosenMultipleChoice : AnswerChoice
    {
        public int solvingID { get; set; }
    }
}
