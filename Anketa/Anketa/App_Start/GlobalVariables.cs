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
        public static Dictionary<int, String> fetchUsernameIdDictionary()
        {
            return new SurveyContext().Users.Select(x => new { userId = x.UserProfileInfo.Id, userName = x.UserName }).ToDictionary(o => o.userId, o => o.userName);
        } 

        public static int getUserProfileInfoId()
        {
            UserManager<User> applicationUserManager = new UserManager<User>(new UserStore<User>(new SurveyContext()));
            var userIdentity = HttpContext.Current.User.Identity.GetUserId();
            if (userIdentity != null)
            {
                User u = applicationUserManager.FindById(userIdentity);
                UserProfileInfo upi = u.UserProfileInfo;
                return upi.Id;
            }
            else
            {
                return 0;
            }
        }
        public static User getCurrentUser()
        {
            UserManager<User> applicationUserManager = new UserManager<User>(new UserStore<User>(new SurveyContext()));
            var userIdentity = HttpContext.Current.User.Identity.GetUserId();
            if (userIdentity != null)
            {
                User user = applicationUserManager.FindById(userIdentity);
                return user;
            }
            else
            {
                var user = new SurveyContext().Users.First(x => x.UserName == "Unidentified User" );
                return user;
            }
        }
    }
}