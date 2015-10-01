using System;
using System.Collections.Generic;
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
        public string questionText { get; set; }
        public TipPitanja? TipPitanja { get; set; }
        public bool aktivnoPitanje { get; set; }

        public virtual ICollection<Answer> Answer { get; set; }
    }
}