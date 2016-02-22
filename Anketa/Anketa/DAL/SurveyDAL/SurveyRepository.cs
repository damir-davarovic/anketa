using Anketa.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public void updateSurvey(Models.Survey survey)
        {
            sDB.Surveys.Attach(survey);
            var sEntry = sDB.Entry<Survey>(survey);
            sEntry.Property(x => x.surveyName).IsModified = true;
            sEntry.Property(x => x.surveyDescription).IsModified = true;
            sEntry.Property(x => x.surveyActive).IsModified = true;
            sEntry.Property(x => x.editDate).CurrentValue = DateTime.Now;
            sDB.SaveChanges();
        }
    }
}