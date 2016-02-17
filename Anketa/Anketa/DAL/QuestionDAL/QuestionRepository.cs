﻿using Anketa.Models;
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
            // https://msdn.microsoft.com/hr-hr/data/jj592676
            // mali tutorial o Attachu
            try
            {
                sDB.Questions.Attach(question);
                var qEntry = sDB.Entry<Question>(question) ;
                qEntry.State = EntityState.Modified;
                if (question.answer != null)
                {
                    var aEntry = sDB.Entry<Answer>(question.answer.First());
                    aEntry.State = EntityState.Modified;
                    if (question.questionType == TipPitanja.Single)
                    {
                        foreach (AnswerChoiceSingle sChoice in question.answer.First().radioAnswers)
                        {
                            var rEntry = sDB.Entry<AnswerChoiceSingle>(sChoice);
                            rEntry.State = EntityState.Modified;
                        }
                    }
                    if (question.questionType == TipPitanja.Multiple)
                    {
                        foreach (AnswerChoiceMultiple mChoice in question.answer.First().selectAnswers)
                        {
                            var mEntry = sDB.Entry<AnswerChoiceMultiple>(mChoice);
                            mEntry.State = EntityState.Modified;
                        }
                    }
                }
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