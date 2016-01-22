using Anketa.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Anketa.Models
{
    public class Survey : IValidatableObject
    {
        [Index(IsUnique = true)]
        public int surveyID { get; set; }
        [DisplayName("Survey owner")]
        public int ownerID { get; set; }
        [DisplayName("Survey name")]
        [Required(AllowEmptyStrings = false , ErrorMessage = "Survey name is required!")]
        public String surveyName { get; set; }
        [DisplayName("Creation date")]
        public DateTime creationDate { get; set; }
        [DisplayName("Edit date")]
        [UIHint("DateNullable")]
        public DateTime? editDate { get; set; }
        [DisplayName("Survey active")]
        public bool surveyActive { get; set; }
        [DisplayName("Survey description")]
        public String surveyDescription { get; set; }

        //public virtual ICollection<Question> Question { get; set; }

        //https://msdn.microsoft.com/en-us/data/gg193959.aspx
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            SurveyContext surveyContext = new SurveyContext();
            if (surveyContext.Surveys.Any(x => x.surveyName == surveyName && x.surveyID != surveyID))
            {
                yield return new ValidationResult("Survey name is required to be unique.");
            }
        }
    }
}