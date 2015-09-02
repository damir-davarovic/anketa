using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anketa.Models
{
    public class User
    {
        public enum userGender{
            M, F
        }

        public int userID { get; set; }
        public String userFirstName { get; set; }
        public String userLastName { get; set; }
        public userGender userG { get; set;}
        public DateTime birthDate { get; set; }
    }
}