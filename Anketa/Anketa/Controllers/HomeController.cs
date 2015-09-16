using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Anketa.Models;
using Anketa.DAL;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Anketa.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private SurveyContext db = new SurveyContext();

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
            // logika prenesena u SurveyController.Index i u survey.cshtml
            return RedirectToAction("Index", "Surveys");
        }

        public ActionResult CreateSurvey()
        {
            return RedirectToAction("Create", "Surveys");
        }
    }
}