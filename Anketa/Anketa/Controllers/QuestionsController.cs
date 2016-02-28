using Anketa.App_Start;
using Anketa.DAL;
using Anketa.Models;
using Anketa.Models.AjaxModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityFramework.Extensions;
using Anketa.DAL.QuestionDAL;


namespace Anketa.Controllers
{
    public class QuestionsController : Controller
    {
        private SurveyContext db = new SurveyContext();
        private SecurityUtils securityUtil = new SecurityUtils();
        private QuestionService qService = new QuestionService();

        // POST: Surveys/Delete/5
        public ActionResult Delete(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
            if (HttpContext.Request.UrlReferrer.ToString().Contains("Create"))
            {
                ViewBag.questionMessage = 0;
                ViewBag.questionMessage = "Question <i>" + id + "</i> succesfully removed!";
            }
            else
            {
                ViewBag.surveyModelEditMessageType = 1;
                ViewBag.surveyModelEditMessage = "Question <i>" + id + "</i> succesfully removed!";
            }
            TempData["Delete"] = "Question <i>" + id + "</i> succesfully removed!";
            //return RedirectToRoute("Surveys/Edit/4");
            //return Redirect(HttpContext.Request.UrlReferrer.ToString());
            return RedirectToAction("Edit/" + question.SurveyID, "Surveys");
        }
        //[HttpPost]
        //[Obsolete("U viewu se referencira JsonResult metoda _AjaxDeleteQuestion koja obavlja istu stvar, ali preko Ajaxa.", true)]
        //public PartialViewResult _AjaxDeleteQuestion(Question question)
        //{
        //    try
        //    {
        //        db.Questions.Remove(db.Questions.Find(question.questionID));
        //        db.SaveChanges();
        //        ViewBag.AjaxMessageType = 1;
        //        ViewBag.AjaxMessage = "Question <i>" + question.questionID + "</i> succesfully removed!";
        //        return PartialView("~/Views/Shared/GlobalPartials/_AjaxInfoMessage.cshtml");
        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.AjaxMessageType = 0;
        //        ViewBag.AjaxMessage = "Database action failed! " + e.StackTrace;
        //        return PartialView("~/Views/Shared/GlobalPartials/_AjaxInfoMessage.cshtml");
        //    }
        //}

        [HttpPost]
        public JsonResult _AjaxDeleteQuestion(Question question)
        {
            var _AjaxResponseModel = new _AjaxResponseModel();
            try
            {
                qService.updateOrder(question, "DEL");
                db.Questions.Remove(db.Questions.Find(question.questionID));
                db.SaveChanges();
                _AjaxResponseModel.message = "Question succesfully removed!";
                _AjaxResponseModel.type = 1;
                return Json(_AjaxResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                _AjaxResponseModel.message = "Database action failed! " + e.StackTrace;
                _AjaxResponseModel.type = 0;
                return Json(_AjaxResponseModel, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult _AjaxAddQuestion()
        {
            Question question = qService.fetchTemplateQuestion();
            return PartialView("~/Views/Questions/Partials/_QuestionListPartial.cshtml", question);
        }

        /// <summary>
        /// This controller action is completely valid. 
        /// It saves the question as is and returns only AjaxResponseModel with message.
        /// It doesn' bind anything but question back. 
        /// </summary>
        /// <param name="question">Object representing question from view, passed through Ajax in JQuery.</param>
        /// <returns>Returns just the status message which is shown to the user, on the view.</returns>
        [Obsolete("U viewu se referencira Ajax metoda _AjaxSaveQuestionWithFeedback koja obavlja istu stvar, ali vraća već kreirani HTML sa bindanim podacima", true)]
        [HttpPost]
        public ActionResult _AjaxSaveQuestion(Question question)
        {
            var _AjaxResponseModel = new _AjaxResponseModel();
            if (!ModelState.IsValid) // kada se pošalje sve s fronte onda će radit
            {
                var errors = ModelState.Keys.SelectMany(e => ModelState[e].Errors).Select(m => m.ErrorMessage).ToArray();
                foreach (string errorMessage in errors)
                {
                    _AjaxResponseModel.message = _AjaxResponseModel.message + errorMessage + "<br />";
                }
                _AjaxResponseModel.type = 0;
                return Json(_AjaxResponseModel, JsonRequestBehavior.AllowGet);
            }
            //List<ValidationResult> validationResult = securityUtil.TryToValidate(question);
            //if (validationResult.Count() > 0)
            //{
            //    _AjaxResponseModel.message = validationResult[0].ErrorMessage;
            //    _AjaxResponseModel.type = 0;
            //    return Json(_AjaxResponseModel, JsonRequestBehavior.AllowGet);
            //}
            qService.updateOrder(question, null);
            /* Primjer Update */
            if (question.questionID >= 1) // if the question already exists
            {
                _AjaxResponseModel = qService.updateQuestion(question);
                return Json(_AjaxResponseModel, JsonRequestBehavior.AllowGet);
            }
            else // if the question is not inserted yet
            {
                try
                {
                    db.Questions.Add(question);
                    db.SaveChanges();

                    _AjaxResponseModel.questionId = question.questionID;
                    _AjaxResponseModel.surveyId = question.SurveyID;
                    if (question.questionType != TipPitanja.Description)
                    {
                        _AjaxResponseModel.answerId = question.answer.First().answerID;
                    }
                    _AjaxResponseModel.message = "Question succesfully added!";
                    _AjaxResponseModel.type = 1;
                    return Json(_AjaxResponseModel, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    _AjaxResponseModel.message = "Database action failed! " + e.StackTrace;
                    _AjaxResponseModel.type = 0;
                    return Json(_AjaxResponseModel, JsonRequestBehavior.AllowGet);
                }
            }
        }

        /// <summary>
        /// This metod is completely valid.
        /// Compared to <see cref="_AjaxSaveQuestion"> it recreates complete view in controller and
        /// passes the result back to view, so it can replace the existing question div with already binded all the IDs and stuff. 
        /// </summary>
        /// <param name="question">Question object, sent from view.</param>
        /// <returns>The whole DIV , meant to replace the existing.</returns>
        [HttpPost]
        public JsonResult _AjaxSaveQuestionWithFeedback(Question question)
        {
            var _AjaxResponseModel = new _AjaxResponseModel();
            if (!ModelState.IsValid) // kada se pošalje sve s fronte onda će radit
            {
                var errors = ModelState.Keys.SelectMany(e => ModelState[e].Errors).Select(m => m.ErrorMessage).ToArray();
                foreach (string errorMessage in errors)
                {
                    _AjaxResponseModel.message = _AjaxResponseModel.message + errorMessage + "<br />";
                }
                _AjaxResponseModel.type = 1;
                return Json(_AjaxResponseModel, JsonRequestBehavior.AllowGet);
            }
            qService.updateOrder(question, null);
            if (question.questionID >= 1) // if the question already exists
            {
                _AjaxResponseModel = qService.updateQuestion(question);
                _AjaxResponseModel.stringData = UtilitiesClass.RenderViewToString(this.ControllerContext, "~/Views/Questions/Partials/_QuestionListPartial.cshtml", question);
                return Json(_AjaxResponseModel, JsonRequestBehavior.AllowGet);
            }
            else // if the question is not inserted yet
            {
                try
                {
                    _AjaxResponseModel = qService.insertQuestion(question);

                    _AjaxResponseModel.stringData = UtilitiesClass.RenderViewToString(this.ControllerContext, "~/Views/Questions/Partials/_QuestionListPartial.cshtml", question);
                    return Json(_AjaxResponseModel, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    _AjaxResponseModel.message = "Database action failed! " + e.StackTrace;
                    _AjaxResponseModel.type = 1;
                    return Json(_AjaxResponseModel, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}