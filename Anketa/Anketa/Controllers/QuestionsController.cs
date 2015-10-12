using Anketa.DAL;
using Anketa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Anketa.Controllers
{
    public class QuestionsController : Controller
    {
        private SurveyContext db = new SurveyContext();

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

        public PartialViewResult _AjaxDeleteQuestion(Question question)
        {
            //db.Questions.Remove(db.Questions.Find(question.questionID));
            //db.SaveChanges();
            ViewBag.AjaxMessageType = 0;
            ViewBag.AjaxMessage = "Question <i>" + question.questionID + "</i> succesfully removed!";
            return PartialView("~/Views/Shared/GlobalPartials/_AjaxInfoMessage.cshtml");
        }

    }
}