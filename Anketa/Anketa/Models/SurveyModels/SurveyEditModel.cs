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
        #region constructors
        public SurveyEditModel()
        {
            surveyModel = null;
            questionsModel = null;
        }
        public SurveyEditModel(int? surveyId)
        {
            surveyModel = new SurveyRepository().fetchSurveyById(surveyId);
            questionsModel = new QuestionRepository().fetchQuestionsBySurveyId(surveyId);
        }
        #endregion constructors
        public Survey surveyModel { get; set; }
        public IEnumerable<Question> questionsModel { get; set; }
    }
}