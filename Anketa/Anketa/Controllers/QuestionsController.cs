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
        private QuestionRepository qRepo = new QuestionRepository();

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
            return RedirectToAction("Edit/"+question.SurveyID, "Surveys");
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
            Question question = qRepo.fetchTemplateQuestion();
            ViewBag.AjaxMessageType = 1;
            ViewBag.AjaxMessage = "New question added";
            ViewBag.li = "listIt";
            return PartialView("~/Views/Questions/Partials/_QuestionListPartial.cshtml", question );
        }
        [HttpPost]
        public ActionResult _AjaxSaveQuestion(Question question)
        {
            var _AjaxResponseModel = new _AjaxResponseModel();
            if (!ModelState.IsValid) // kada se pošalje sve s fronte onda će radit
            {
                var errors = ModelState.Keys.SelectMany(e => ModelState[e].Errors).Select(m => m.ErrorMessage).ToArray();
                foreach (string errorMessage in errors)
                {
                    _AjaxResponseModel.message = _AjaxResponseModel.message + errorMessage + "\n";
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
            qRepo.updateOrder(question);
            /* Primjer Update */
            if (question.questionID >= 1) // if the question already exists
            {
                _AjaxResponseModel = qRepo.updateQuestion(question);
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
    }
}