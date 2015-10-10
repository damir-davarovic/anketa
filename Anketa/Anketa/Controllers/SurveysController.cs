using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Anketa.DAL;
using Anketa.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Anketa.App_Start;
using Anketa.DAL.QuestionDAL;
using Anketa.Models.SurveyModels;
using System.ComponentModel.DataAnnotations;

//Ovaj cijeli controller se generiro sam.

namespace Anketa.Controllers
{
    public class SurveysController : Controller
    {
        private SurveyContext db = new SurveyContext();
        private SecurityUtils securityUtil = new SecurityUtils();

        // GET: Surveys
        public ActionResult Index()
        {
            var model = new SurveyIndexModel();
            return View(model);
        }

        // GET: Surveys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // GET: Surveys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Surveys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "surveyID,ownerID,surveyName,creationDate,surveyActive")] Survey survey)
        public ActionResult Create([Bind(Include = "ownerID,surveyName,surveyActive,surveyDescription")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                survey.creationDate = DateTime.Now;
                survey.ownerID = GlobalVariables.getUserProfileInfoId();
                db.Surveys.Add(survey);
                db.SaveChanges();
                return RedirectToAction("Edit/"+survey.surveyID);
            }

            return View(survey);
        }

        // GET: Surveys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyEditModel surveyModel = new SurveyEditModel(id);
            //Survey survey = db.Surveys.Find(id);
            //QuestionRepository qRepo = new QuestionRepository();
            //IEnumerable<Question> questions = qRepo.fetchQuestionsBySurveyId(survey.surveyID);
            if (surveyModel.surveyModel == null)
            {
                return HttpNotFound();
            }
            //var tuple = new Tuple<Survey, IEnumerable<Question>>(survey, questions);
            // so far we have fetched the survey and it's questions
            // we have to translate them to a view
            //return View(survey);
            if (HttpContext.Request.UrlReferrer.ToString().Contains("Create"))
            {
                ViewBag.surveyModelEditMessageType = 1;
                ViewBag.surveyModelEditMessage = "Survey <i>" + surveyModel.surveyModel.surveyName + "</i> succesfully created!";
            }
            return View(surveyModel);
        }

        // POST: Surveys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind] Survey survey)
        {
            //if (ModelState.IsValid)
            //{
            //db.Entry(survey).State = EntityState.Modified;
            //db.SaveChanges();
            //return RedirectToAction("Index");
            //}
            SurveyEditModel surveyEditModel = new SurveyEditModel();
            surveyEditModel.surveyModel = survey;
            if (!securityUtil.TryToValidate(survey))
            {
                ViewBag.surveyModelEditMessageType = 0;
                ViewBag.surveyModelEditMessage = "Validation error on <i>" + surveyEditModel.surveyModel.surveyName + "</i>!";
                return View(new SurveyEditModel(survey.surveyID));
            }
            db.Surveys.Attach(survey);
            var entry = db.Entry<Survey>(survey);
            entry.Property(x => x.surveyName).IsModified = true;
            entry.Property(x => x.surveyDescription).IsModified = true;
            entry.Property(x => x.surveyActive).IsModified = true;
            entry.Property(x => x.editDate).CurrentValue = DateTime.Now;
            db.SaveChanges();
            surveyEditModel = new SurveyEditModel(survey.surveyID);
            ViewBag.surveyModelEditMessageType = 1;
            ViewBag.surveyModelEditMessage = "Survey <i>" + surveyEditModel.surveyModel.surveyName + "</i> succesfully changed!";
            return View(surveyEditModel);
            //var surveyName = entry.OriginalValues["surveyName"].ToString();
            //var surveyNameNew = entry.CurrentValues["surveyName"].ToString();
            //var suName = entry.Property(x => x.surveyName).CurrentValue;
            //var suNameO = entry.Property(x => x.surveyName).OriginalValue;
            //if(entry.OriginalValues["surveyName"] != entry.CurrentValues["surveyName"])
            //{
            //    entry.Property(x => x.surveyName).IsModified = true;
            //}
            //if (entry.OriginalValues["surveyDescription"] != entry.CurrentValues["surveyDescription"])
            //{
            
            //}
            //if (entry.OriginalValues["surveyActive"] != entry.CurrentValues["surveyActive"])
            //{
            
            //}
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TestAjax([Bind] Survey survey)
        {
            // trebam skužit kak Ajax updatea polja na viewu. Ovo niš ne updatea.
            SurveyEditModel surveyEditModel = new SurveyEditModel();
            //surveyEditModel.surveyModel = survey;
            //if (!securityUtil.TryToValidate(survey))
            //{
            //    surveyEditModel.surveyModel.surveyModelEditMessage = "Validation error on "+surveyEditModel.surveyModel.surveyName+"!";
            //    return View(new SurveyEditModel(survey.surveyID));
            //}
            //db.Surveys.Attach(survey);
            //var entry = db.Entry<Survey>(survey);
            //entry.Property(x => x.surveyName).IsModified = true;
            //entry.Property(x => x.surveyDescription).IsModified = true;
            //entry.Property(x => x.surveyActive).IsModified = true;
            //entry.Property(x => x.editDate).CurrentValue = DateTime.Now;
            //db.SaveChanges();
            surveyEditModel = new SurveyEditModel(survey.surveyID);
            //surveyEditModel.surveyModel.surveyModelEditMessage = "Survey " + surveyEditModel.surveyModel.surveyName + " succesfully changed!";
            return View(surveyEditModel);
        }



        // GET: Surveys/Solve/5
        public ActionResult Solve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }

            List<Question> result = db.Questions.Where(o => o.SurveyID == survey.surveyID).ToList();

            return View(result);
        }


        // GET: Surveys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // POST: Surveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Survey survey = db.Surveys.Find(id);
            db.Surveys.Remove(survey);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
