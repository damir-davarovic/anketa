using Anketa.Models;
using Anketa.Models.AjaxModels;
using Anketa.Models.AnswerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anketa.DAL.QuestionDAL
{
    public class QuestionService
    {
        private QuestionRepository qRepo = new QuestionRepository();

        public Question fetchTemplateQuestion()
        {
            return qRepo.fetchTemplateQuestion();
        }

        public void templetizeQuestionAnswer(Question question)
        {
            if (question.questionType == TipPitanja.Description && question.answer == null)
            {
                List<Answer> templateAnswer = new List<Answer>();

                templateAnswer.Add(new Answer()
                {
                    radioAnswers = new List<AnswerChoiceSingle>() { new AnswerChoiceSingle() },
                    selectAnswers = new List<AnswerChoiceMultiple>() { new AnswerChoiceMultiple() }
                });
                question.answer = templateAnswer;
            }
        }

        public IEnumerable<Anketa.Models.Question> fetchQuestionsBySurveyId(int surveyId)
        {
            return qRepo.fetchQuestionsBySurveyId(surveyId);
        }

        public void updateOrder(Question question, string type)
        {
            qRepo.updateOrder(question, type);
        }

        public _AjaxResponseModel updateQuestion(Question question)
        {
            var _ajaxResponseModel = new _AjaxResponseModel();
            try
            {
                qRepo.updateQuestion(question);
                templetizeQuestionAnswer(question);
                _ajaxResponseModel.message = "Question succesfully changed!";
                return _ajaxResponseModel;
            }
            catch (Exception e)
            {
                _ajaxResponseModel.message = "Database action failed! " + e.StackTrace;
                _ajaxResponseModel.type = 1;
                return _ajaxResponseModel;
            }
        }

        public _AjaxResponseModel insertQuestion(Question question)
        {
            var _ajaxResponseModel = new _AjaxResponseModel();
            try
            {
                qRepo.insertQuestion(question);
                templetizeQuestionAnswer(question);
                _ajaxResponseModel.message = "Question succesfully added!";
                return _ajaxResponseModel;
            }
            catch (Exception e)
            {
                _ajaxResponseModel.message = "Database action failed! " + e.StackTrace;
                _ajaxResponseModel.type = 1;
                return _ajaxResponseModel;
            }
        }
    }
}