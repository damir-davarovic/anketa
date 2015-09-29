using System;
using Anketa.App_Start;

namespace Anketa.Models.SurveyModels
{
    public class SurveyCreateModel
    {
        public User userObj = GlobalVariables.getCurrentUser();
        public Survey surveyObj { get; set; }
    }
}