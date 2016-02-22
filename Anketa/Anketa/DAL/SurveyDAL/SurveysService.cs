using Anketa.Models;
using Anketa.Models.AjaxModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anketa.DAL.SurveyDAL
{
    public class SurveysService
    {
        private SurveyRepository sRepo = new SurveyRepository();
        private _AjaxResponseModel ajaxResponse = new _AjaxResponseModel();

        public _AjaxResponseModel updateSurvey(Survey survey)
        {
            try
            {
                sRepo.updateSurvey(survey);
                ajaxResponse.message = "Survey "+survey.surveyName+" successfully saved";
                ajaxResponse.surveyId = survey.surveyID;
                return ajaxResponse;
            }
            catch (Exception e)
            {
                ajaxResponse.type = 1;
                ajaxResponse.message = "Database action failed!\n" + e.Message;
                return ajaxResponse;
            }
        }

    }
}