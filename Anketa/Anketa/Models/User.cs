using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anketa.Models
{
    public class User
    {
        public int userID { get; set; }
        public String userFirstName { get; set; }
        public String userLastName { get; set; }
        public enum userGender { M, F };
        public DateTime birthDate { get; set; }
    }
}