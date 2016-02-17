using Anketa.Models.AnswerModels;
using Anketa.Models.ExceptionModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Anketa.DAL.AnswersDAL
{
    public class AnswersRepository
    {
        private SurveyContext sDB = new SurveyContext();

        public void _AjaxDeleteSingleChoice(AnswerChoiceSingle pChoiceItem)
        {
            try
            {
                sDB.AnswerChoiceSingle.Attach(pChoiceItem);
                var choiceEntry = sDB.Entry<AnswerChoiceSingle>(pChoiceItem);
                choiceEntry.State = EntityState.Deleted;
                sDB.SaveChanges();
            }
            catch (Exception tException)
            {
                throw new SurveyRuntimeException("Deletion of choice item failed! Reason: " + tException.Message);
            }
        }
    }
}