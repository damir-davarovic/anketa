using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Anketa.Models.SurveySolvedModels
{
    public class SurveySolved
    {
        [Key]
        [Index(IsUnique = true)]
        public int solvingID { get; set; }
        public int surveyID { get; set; }
        [DisplayName("Solving date")]
        public DateTime solvingDate { get; set; }
        public int userSolvedID { get; set; }
    }
}