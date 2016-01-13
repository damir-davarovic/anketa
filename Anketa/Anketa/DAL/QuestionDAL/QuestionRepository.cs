using Anketa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityFramework.Extensions;
using Anketa.Models.AjaxModels;

namespace Anketa.DAL.QuestionDAL
{
    public class QuestionRepository
    {
        public SurveyContext sDB = new SurveyContext();
        public IEnumerable<Anketa.Models.Question> fetchQuestionsBySurveyId(int surveyId)
        {
            return sDB.Questions.Where(x => x.SurveyID == surveyId).OrderBy(s => s.questionOrder) ;
        }
        public void updateOrder(Question question)
        {
            sDB.Questions.Where(q => q.questionOrder >= question.questionOrder).Update(q => new Question { questionOrder = q.questionOrder + 1 });
        }
        public _AjaxResponseModel updateQuestion(Question question)
        {
            var _AjaxResponseModel = new _AjaxResponseModel();
            sDB.Questions.Attach(question);
            var entry = sDB.Entry<Question>(question);
            entry.Property(x => x.questionText).IsModified = true;
            entry.Property(x => x.aktivnoPitanje).IsModified = true;
            entry.Property(x => x.TipPitanja).IsModified = false;
            entry.Property(x => x.questionOrder).IsModified = true;
            try
            {
                sDB.SaveChanges();
                _AjaxResponseModel.type = 1;
                _AjaxResponseModel.message = "Question succesfully changed!";
                return _AjaxResponseModel;
            }
            catch (Exception e)
            {
                _AjaxResponseModel.message = "Database action failed! " + e.StackTrace;
                _AjaxResponseModel.type = 0;
                return _AjaxResponseModel;
            }
        }
    }
}