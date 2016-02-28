using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anketa.Models.AnswerModels;

namespace Anketa.Models.SurveySolvedModels
{
    public class ChosenSingleChoice : AnswerChoice
    {
        public int solvingID { get; set; }
    }
}
