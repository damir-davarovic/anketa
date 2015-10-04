using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anketa.DAL
{
    public class SurveyRepository
    {
        private SurveyContext sDB = new SurveyContext();

        public IEnumerable<Anketa.Models.Survey> fetchAllSurveys() 
        {
            return sDB.Surveys.ToList();
        }

        public Anketa.Models.Survey fetchSurveyById(int? surveyId)
        {
            return sDB.Surveys.Find(surveyId);
        }
    }
}