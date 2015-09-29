using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Anketa.DAL;
using Anketa.App_Start;

namespace Anketa.Models
{
    public class SurveyIndexModel
    {
        public Dictionary<int, string> dictUser = GlobalVariables.fetchUsernameIdDictionary();
        public IEnumerable<Survey> allSurveysList = SurveyRepository.fetchAllSurveys();
        public int currentUser = GlobalVariables.getUserProfileInfoId();
        public Survey surveyModel = new Survey();
    }
}