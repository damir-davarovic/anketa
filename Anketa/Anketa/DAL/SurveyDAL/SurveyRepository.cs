using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anketa.DAL
{
    public static class SurveyRepository
    {
        private static SurveyContext sDB = new SurveyContext();

        public static IEnumerable<Anketa.Models.Survey> fetchAllSurveys() 
        {
            return sDB.Surveys.ToList();
        }
    }
}