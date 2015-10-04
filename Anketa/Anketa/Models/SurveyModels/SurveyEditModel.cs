using Anketa.DAL;
using Anketa.DAL.QuestionDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anketa.Models.SurveyModels
{
    public class SurveyEditModel
    {

        public SurveyEditModel(int? surveyId)
        {
            surveyModel = new SurveyRepository().fetchSurveyById(surveyId);
            questionsModel = new QuestionRepository().fetchQuestionsBySurveyId(surveyId);
        }

        public Survey surveyModel { get; set; }
        public IEnumerable<Question> questionsModel { get; set; }
    }
}