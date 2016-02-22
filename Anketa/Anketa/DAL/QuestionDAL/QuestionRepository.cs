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
        public void updateOrder(Question question , string type)
        {
            if (type == "DEL")
            {
                sDB.Questions.Where(q => q.questionOrder >= question.questionOrder).Update(q => new Question { questionOrder = q.questionOrder -1 });
            }
            else
            {
                sDB.Questions.Where(q => q.questionOrder >= question.questionOrder).Update(q => new Question { questionOrder = q.questionOrder + 1 });
            }
        }

        public void updateQuestion(Question question)
        {
            List<AnswerChoiceSingle> tempListSingle = new List<AnswerChoiceSingle>();
            List<AnswerChoiceMultiple> tempListMultiple = new List<AnswerChoiceMultiple>();
            Answer qAnswer = null;

            // https://msdn.microsoft.com/hr-hr/data/jj592676
            // mali tutorial o Attachu

                if (question.answer != null)
                {
                    qAnswer = question.answer.First();

                    tempListSingle = (List<AnswerChoiceSingle>)qAnswer.radioAnswers;
                    qAnswer.radioAnswers = null;

                    tempListMultiple = (List<AnswerChoiceMultiple>)qAnswer.selectAnswers;
                    qAnswer.selectAnswers = null;
                }

                sDB.Questions.Attach(question);
                var qEntry = sDB.Entry<Question>(question);
                qEntry.State = EntityState.Modified;                

                if (question.answer != null)
                {
                    var aEntry = sDB.Entry<Answer>(qAnswer);
                    aEntry.State = EntityState.Modified;
                    if (question.questionType == TipPitanja.Single)
                    {
                        foreach (AnswerChoiceSingle sChoice in tempListSingle)
                        {
                            sChoice.answerID = qAnswer.answerID;
                            var rEntry = sDB.Entry<AnswerChoiceSingle>(sChoice);
                            if (sChoice.choiceId == 0)
                            {
                                rEntry.State = EntityState.Added;
                            }
                            else
                            {
                                rEntry.State = EntityState.Modified;
                            }
                        }
                    }
                    if (question.questionType == TipPitanja.Multiple)
                    {
                        foreach (AnswerChoiceMultiple mChoice in tempListMultiple)
                        {
                            mChoice.answerID = qAnswer.answerID;
                            var mEntry = sDB.Entry<AnswerChoiceMultiple>(mChoice);
                            if (mChoice.choiceId == 0)
                            {
                                mEntry.State = EntityState.Added;
                            }
                            else
                            {
                                mEntry.State = EntityState.Modified;
                            }
                        }
                    }
                }
                sDB.SaveChanges();
        }

        public void insertQuestion(Question question)
        {
            List<AnswerChoiceSingle> tempListSingle = new List<AnswerChoiceSingle>();
            List<AnswerChoiceMultiple> tempListMultiple = new List<AnswerChoiceMultiple>();
            Answer qAnswer = null;

            if (question.answer != null)
            {
                qAnswer = question.answer.First();
                tempListSingle = (List<AnswerChoiceSingle>)qAnswer.radioAnswers;
                qAnswer.radioAnswers = null;

                tempListMultiple = (List<AnswerChoiceMultiple>)qAnswer.selectAnswers;
                qAnswer.selectAnswers = null;

                question.answer = null;
            }

            sDB.Questions.Attach(question);
            var qEntry = sDB.Entry<Question>(question);
            qEntry.State = EntityState.Added;
            sDB.SaveChanges();

            if (qAnswer != null)
            {
                qAnswer.questionID = question.questionID;
                var aEntry = sDB.Entry<Answer>(qAnswer);
                if (qAnswer.answerID != 0)
                {
                    aEntry.State = EntityState.Modified;
                }
                else
                {
                    aEntry.State = EntityState.Added;
                }
                sDB.SaveChanges();

                if (question.questionType == TipPitanja.Single)
                {
                    foreach (AnswerChoiceSingle sChoice in tempListSingle)
                    {
                        sChoice.answerID = qAnswer.answerID;
                        var rEntry = sDB.Entry<AnswerChoiceSingle>(sChoice);
                        if (sChoice.choiceId == 0)
                        {
                            rEntry.State = EntityState.Added;
                        }
                        else
                        {
                            rEntry.State = EntityState.Modified;
                        }
                    }
                }
                if (question.questionType == TipPitanja.Multiple)
                {
                    foreach (AnswerChoiceMultiple mChoice in tempListMultiple)
                    {
                        mChoice.answerID = qAnswer.answerID;
                        var mEntry = sDB.Entry<AnswerChoiceMultiple>(mChoice);
                        if (mChoice.choiceId == 0)
                        {
                            mEntry.State = EntityState.Added;
                        }
                        else
                        {
                            mEntry.State = EntityState.Modified;
                        }
                    }
                }
            }
            sDB.SaveChanges();
        }
    }
}