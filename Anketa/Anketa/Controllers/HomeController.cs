using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Anketa.Models;

namespace Anketa.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Application for solving and creating your own surveys";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact page.";
            return View();
        }

        public ActionResult Survey()
        {
            var survey = new Survey
            {
                surveyName = "Testna anketa!",
                surveyActive = true,
                Question = new List<Question>(),
                creationDate = new DateTime(),
                ownerID = "aaa",
                surveyID = 1
            };
            return View(survey);
        }

        public ActionResult CreateSurvey()
        {
            return RedirectToAction("Create", "Surveys");
            //View();
        }
    }
}