﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

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
            return View();
        }

        public ActionResult CreateSurvey()
        {
            var userId = User.Identity.GetUserId();
            ViewBag.Message = userId;
            return View();
        }
    }
}