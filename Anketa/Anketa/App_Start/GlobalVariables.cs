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

namespace Anketa.App_Start
{
    public static class GlobalVariables
    {
        public static Dictionary<int, String> userNameIdDictionary = new ApplicationDbContext().Users.Select(x => new { userId = x.UserProfileInfo.Id, userName = x.UserName }).ToDictionary(o => o.userId, o => o.userName);

        public static int getCurrentUser()
        {
            UserManager<ApplicationUser> applicationUserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var userIdentity = HttpContext.Current.User.Identity.GetUserId();
            if (userIdentity != null)
            {
                return applicationUserManager.FindById(userIdentity).UserProfileInfo.Id;
            }
            else
            {
                return 0;
            }
        }
    }
}