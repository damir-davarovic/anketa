using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Anketa.App_Start
{
    public class SecurityUtils
    {
        public List<ValidationResult> TryToValidate(object objectForValidation)
        {
            var context = new ValidationContext(objectForValidation);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(
                objectForValidation, context, results
            );
            return results;
        }
    }
}