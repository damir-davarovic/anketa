﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anketa.DAL.QuestionDAL
{
    public class QuestionRepository
    {
        public SurveyContext surveyContext = new SurveyContext();
        public IEnumerable<Anketa.Models.Question> fetchQuestionsBySurveyId(int surveyId)
        {
            return surveyContext.Questions.Where(x => x.SurveyID == surveyId);
        }
    }
}