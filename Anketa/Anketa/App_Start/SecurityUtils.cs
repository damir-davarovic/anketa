using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Anketa.App_Start
{
    public class SecurityUtils
    {
        public bool TryToValidate(object objectForValidation)
        {
            var context = new ValidationContext(objectForValidation);
            var results = new List<ValidationResult>();
            return Validator.TryValidateObject(
                objectForValidation, context, results
            );
        }
    }
}