using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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