using Anketa.Models;
using Anketa.Models.AjaxModels;
using Anketa.Models.AnswerModels;
using Anketa.Models.ExceptionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anketa.DAL.AnswersDAL
{
    public class AnswersService
    {
        private AnswersRepository aRepo = new AnswersRepository();
        private _AjaxResponseModel ajaxReponse = new _AjaxResponseModel() {type = 1};

        public _AjaxResponseModel _AjaxDeleteSingleChoice(AnswerChoiceSingle pChoiceItem)
        {
            try
            {
                aRepo._AjaxDeleteSingleChoice(pChoiceItem);
                ajaxReponse.message = "Answer choice succesfully deleted!";
            }
            catch (SurveyRuntimeException tException)
            {
                ajaxReponse.message = tException.Message;
                ajaxReponse.type = 1;
            }
            return ajaxReponse;
        }
    }
}