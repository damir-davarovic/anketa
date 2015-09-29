using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Anketa.Models
{
    public class Survey
    {
        [Index(IsUnique = true)]
        public int surveyID { get; set; }
        [DisplayName("Survey owner")]
        public int ownerID { get; set; }
        [DisplayName("Survey name")]
        public String surveyName { get; set; }
        [DisplayName("Creation date")]
        public DateTime creationDate { get; set; }
        [DisplayName("Edit date")]
        [UIHint("DateNullable")]
        public DateTime? editDate { get; set; }
        [DisplayName("Survey active")]
        public bool surveyActive { get; set; }
        [DisplayName("Survey region")]
        public String surveyRegion { get; set; }

        public virtual ICollection<Question> Question { get; set; }
    }
}