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
    public enum TipPitanja
    {
        Description, Multiple, Single, Scale
    }

    public class Question
    {
        [Index(IsUnique = true)]
        public int questionID { get; set; }
        public int SurveyID { get; set; }
        [DisplayName("Question text")]
        public string questionText { get; set; }
        [DisplayName("Question type")]
        public TipPitanja? TipPitanja { get; set; }
        [DisplayName("Question active")]
        public bool aktivnoPitanje { get; set; }

        public virtual ICollection<Answer> Answer { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            SurveyContext surveyContext = new SurveyContext();
            if (questionText == null || questionText.Trim() == "")
            {
                yield return new ValidationResult("Question text is not allowed to be empty!");
            }
        }
    }
}