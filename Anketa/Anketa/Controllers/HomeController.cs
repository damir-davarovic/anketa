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
            //var currentUser = 0;
            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            //var userIdentity = User.Identity.GetUserId();
            //if (userIdentity != null)
            //{
            //    currentUser = manager.FindById(userIdentity).UserProfileInfo.Id;
            //}
            // ovo je premješteno u survey.cshtml
            ViewBag.User = 0;
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var userIdentity = User.Identity.GetUserId();
            if (userIdentity != null)
            {
                ViewBag.User = manager.FindById(userIdentity).UserProfileInfo.Id;
            } 
            return View(db.Surveys.ToList());
        }

        public ActionResult CreateSurvey()
        {
            return RedirectToAction("Create", "Surveys");
            //View();
        }
    }
}