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
using Anketa.Models.AjaxModels;
using Anketa.DAL.SurveyDAL;

//Ovaj cijeli controller se generiro sam.

namespace Anketa.Controllers
{
    public class SurveysController : Controller
    {
        private SurveyContext db = new SurveyContext();
        private SecurityUtils securityUtil = new SecurityUtils();
        private QuestionRepository qRepo = new QuestionRepository();
        private SurveysService sService = new SurveysService();
        private QuestionService qService = new QuestionService();

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
                TempData["Create"] = "Survey <i> " + survey.surveyName + "</i> succesfully created!";
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
            SurveyEditModel surveyModel = new SurveyEditModel((int)id);
            if (surveyModel.surveyModel == null)
            {
                return HttpNotFound();
            }
            if (HttpContext.Request.UrlReferrer != null)
            {
                if (TempData.Keys.Contains("Create"))
                {
                    ViewBag.surveyModelEditMessageType = 1;
                    ViewBag.surveyModelEditMessage = TempData["Create"];
                }
                else if (TempData.Keys.Contains("RedirectToEdit"))
                {
                    ViewBag.surveyModelEditMessageType = 1;
                    ViewBag.surveyModelEditMessage = TempData["RedirectToEdit"];
                }
                else if (TempData.Keys.Contains("Delete"))
                {
                    ViewBag.surveyModelEditMessageType = 1;
                    ViewBag.surveyModelEditMessage = TempData["Delete"];
                }
            }            
            return View(surveyModel);
        }

        // GET: Surveys/Edit/5
        public ActionResult RediredtToEdit(int? id)
        {
            SurveyEditModel surveyModel = new SurveyEditModel((int)id);
            if (surveyModel.surveyModel == null)
            {
                return HttpNotFound();
            }
            TempData["RedirectToEdit"] = "Survey " + surveyModel.surveyModel.surveyName + "</i> succesfully updated!";
            return RedirectToAction("Edit/"+id);
        }

        // POST: Surveys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete("U viewu se referencira Ajax metoda _AjaxEdit koja obavlja istu stvar, ali preko Ajaxa.", true)]
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
            if (securityUtil.TryToValidate(survey).Count() > 0)
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
        }

        public ActionResult _AjaxEdit([Bind] Survey survey)
        {
            var _AjaxResponseModel = new _AjaxResponseModel();

            #region Validation
            if (!ModelState.IsValid)
                // ovdje bih htio uguzit, odnosno zamijenit server sa client-side validacijom
                // ja sam sad ovdje iskoristio ugrađenu validaciju i vratio customizirani rezultat
                // ALI!! to se može odradit i bez naše intervencije, samo nikako ne vidim kako
                // pogledaj primjer kad se želiš loginat - to ti je 
                // public async Task<ActionResult> Login(LoginViewModel model, string returnUrl) iz AccountController
                // View je Login.cshtml
                //https://www.google.hr/webhp?sourceid=chrome-instant&ion=1&espv=2&es_th=1&ie=UTF-8#q=c%23+mvc+5+client+side+validation+partial+view
                //https://xhalent.wordpress.com/2011/01/24/applying-unobtrusive-validation-to-dynamic-content/
                //http://www.asp.net/mvc/overview/getting-started/introduction/adding-validation
                // ove js-ove sam već dodo i pokrenu se, ali iz nekog razloga mi form.valid() svaki puta vraća true... Ne mogu skužit trebam li za klijentsku validaciju
                // poseban jquery sam napisat ili mogu nekako koristit ovaj ugrađeni MVC-ov... 

            {
                var errorDictionary = ModelState.Where(y => y.Value.Errors.Count > 0).ToDictionary(y => y.Key, y => y.Value.Errors.Select(e => e.ErrorMessage).ToArray());
                foreach (KeyValuePair<string, string[]> validationEntry in errorDictionary)
                {
                    _AjaxResponseModel.message = _AjaxResponseModel.message + validationEntry.Value.First().ToString() + "\n";
                }
                _AjaxResponseModel.type = 2;
                return Json(_AjaxResponseModel, JsonRequestBehavior.AllowGet);
            }
            List<ValidationResult> validationResult = securityUtil.TryToValidate(survey);
            if (validationResult.Count() > 0)
            {
                _AjaxResponseModel.message = validationResult[0].ErrorMessage;
                _AjaxResponseModel.type = 0;
                return Json(_AjaxResponseModel, JsonRequestBehavior.AllowGet);
            }
            #endregion Validation

            db.Surveys.Attach(survey);
            var entry = db.Entry<Survey>(survey);
            entry.Property(x => x.surveyName).IsModified = true;
            entry.Property(x => x.surveyDescription).IsModified = true;
            entry.Property(x => x.surveyActive).IsModified = true;
            entry.Property(x => x.editDate).CurrentValue = DateTime.Now;
            try{
                db.SaveChanges();
                _AjaxResponseModel.message = "Survey <i>" + survey.surveyName + "</i> succesfully changed!";
                _AjaxResponseModel.type = 1;
                return Json(_AjaxResponseModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e){
                _AjaxResponseModel.message = "Database action failed!/n" + e.StackTrace;
                _AjaxResponseModel.type = 0;
                return Json(_AjaxResponseModel, JsonRequestBehavior.AllowGet);
            }            
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

        [HttpPost]
        public ActionResult SaveSurvey(SurveyEditModel fullSurvey)
        {
            _AjaxResponseModel _ajaxResponse = new _AjaxResponseModel();
            Survey incomingSurvey = fullSurvey.surveyModel;
            IEnumerable<Question> incomingQuestion = fullSurvey.questionsModel;

            #region Validation
            if (!ModelState.IsValid)
            {
                var errorDictionary = ModelState.Where(y => y.Value.Errors.Count > 0).ToDictionary(y => y.Key, y => y.Value.Errors.Select(e => e.ErrorMessage).ToArray());
                foreach (KeyValuePair<string, string[]> validationEntry in errorDictionary)
                {
                    _ajaxResponse.message = _ajaxResponse.message + validationEntry.Value.First().ToString() + "\n";
                }
                _ajaxResponse.type = 1;
                return Json(_ajaxResponse, JsonRequestBehavior.AllowGet);
            }
            List<ValidationResult> validationResult = securityUtil.TryToValidate(incomingSurvey);
            if (validationResult.Count() > 0)
            {
                _ajaxResponse.message = validationResult[0].ErrorMessage;
                _ajaxResponse.type = 1;
                return Json(_ajaxResponse, JsonRequestBehavior.AllowGet);
            }
            #endregion Validation


            _ajaxResponse = sService.updateSurvey(incomingSurvey);
            if (incomingQuestion != null)
            {
                foreach (Question questionItem in incomingQuestion)
                {
                    if (questionItem.questionID == 0)
                    {
                        _ajaxResponse = qService.insertQuestion(questionItem);
                    }
                    else
                    {
                        _ajaxResponse = qService.updateQuestion(questionItem);
                    }
                    if (_ajaxResponse.type != 0)
                    {
                        return Json(_ajaxResponse, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(_ajaxResponse, JsonRequestBehavior.AllowGet);
            // this is not a final return. AJAX response redirects this to RediredtToEdit
        }
    }
}
