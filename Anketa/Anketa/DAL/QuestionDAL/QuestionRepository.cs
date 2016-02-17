using Anketa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityFramework.Extensions;
using Anketa.Models.AjaxModels;
using System.Collections;
using Anketa.Models.AnswerModels;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace Anketa.DAL.QuestionDAL
{
    public class QuestionRepository
    {
        public SurveyContext sDB = new SurveyContext();
        /// <summary>
        /// Method which generates some template Question object with
        /// basic not null objects so that the _AjaxAddQuestion()
        /// can add all the basic elements on the partial pages. 
        /// </summary>
        /// <returns>
        /// Question object with basic not null values
        /// </returns>
        public Question fetchTemplateQuestion()
        {
            Question templateQuestion = new Question();
            List<Answer> templateAnswer = new List<Answer>();

            templateAnswer.Add(new Answer() { 
                radioAnswers = new List<AnswerChoiceSingle>() { new AnswerChoiceSingle() },
                selectAnswers = new List<AnswerChoiceMultiple>() { new AnswerChoiceMultiple() }
            });
            templateQuestion.answer = templateAnswer;
            return templateQuestion;
        }
        public IEnumerable<Anketa.Models.Question> fetchQuestionsBySurveyId(int surveyId)
        {
            return sDB.Questions.Where(x => x.SurveyID == surveyId).OrderBy(s => s.questionOrder);
        }
        public void updateOrder(Question question)
        {
            sDB.Questions.Where(q => q.questionOrder >= question.questionOrder).Update(q => new Question { questionOrder = q.questionOrder + 1 });
        }
        public _AjaxResponseModel updateQuestion(Question question)
        {
            var _AjaxResponseModel = new _AjaxResponseModel();
            try
            {
                sDB.Questions.Attach(question);
                var qEntry = sDB.Entry<Question>(question);
                qEntry.Property(x => x.questionText).IsModified = true;
                qEntry.Property(x => x.aktivnoPitanje).IsModified = true;
                qEntry.Property(x => x.questionType).IsModified = false;
                qEntry.Property(x => x.questionOrder).IsModified = true;

                if (question.answer != null)
                {
                    var aEntry = sDB.Entry<Answer>(question.answer.First());
                    aEntry.Property(a => a.answerID).IsModified = false;
                    aEntry.Property(a => a.questionID).IsModified = false;
                    aEntry.Property(a => a.answerText).IsModified = true;
                    aEntry.Property(a => a.minAnswerValue).IsModified = true;
                    aEntry.Property(a => a.maxAnswerValue).IsModified = true;
                }
                //if (question.answer != null)
                //{
                //    var dbQuestion = sDB.Questions.Include(q => q.answer).Single(q => q.questionID == question.questionID);

                //    var qEntry = sDB.Entry(dbQuestion);
                //    qEntry.Property(x => x.questionText).IsModified = true;
                //    qEntry.Property(x => x.aktivnoPitanje).IsModified = true;
                //    qEntry.Property(x => x.questionType).IsModified = false;
                //    qEntry.Property(x => x.questionOrder).IsModified = true;

                //    var aEntry = sDB.Entry(dbQuestion.answer.First());
                //    aEntry.Property(x => x.answerText).IsModified = true;
                //    aEntry.Property(x => x.maxAnswerValue).IsModified = true;
                //    aEntry.Property(x => x.minAnswerValue).IsModified = true;

                //    sDB.SaveChanges();
                //}
                //else
                //{
                //    var dbQuestion = sDB.Questions.Single(q => q.questionID == question.questionID);

                //    var qEntry = sDB.Entry(dbQuestion);
                //    qEntry.Property(x => x.questionText).IsModified = true;
                //    qEntry.Property(x => x.aktivnoPitanje).IsModified = true;
                //    qEntry.Property(x => x.questionType).IsModified = false;
                //    qEntry.Property(x => x.questionOrder).IsModified = true;

                //    sDB.SaveChanges();
                //}
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