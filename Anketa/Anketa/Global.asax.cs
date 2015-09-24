using Anketa.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Anketa
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //// Initializes and seeds the database.
            //Database.SetInitializer(new SurveyInitializer());

            //// Forces initialization of database on model changes.
            //using (var context = new SurveyContext())
            //{
            //    context.Database.Initialize(force: true);
            //}

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            if (typeof(MvcApplication).Assembly.ManifestModule.Name.ToUpper() == "DYNAMICMVC.DLL")
            {
                throw new Exception("Your UI assembly cannot be named DynamicMVC.  This conflicts with dynamicmvc.dll");
            }
            var applicationMetadata = new DynamicMVC.Business.Models.ApplicationMetadata(typeof(MvcApplication).Assembly,
                typeof(MvcApplication).Assembly, typeof(MvcApplication).Assembly,
                () => new DynamicMVC.Data.DynamicRepository(new SurveyContext()));
            DynamicMVC.Managers.DynamicMVCManager.ParseApplicationMetadata(applicationMetadata);

            DynamicMVC.Managers.DynamicMVCManager.SetDynamicRoutes(RouteTable.Routes);
        }
    }
}
