using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anketa.Models
{
    public class Survey
    {
        public int surveyID { get; set; }
        public int ownerId { get; set; }
        public String surveyName { get; set; }
        public DateTime creationDate { get; set; }
        public bool surveyActive { get; set; }

        public virtual ICollection<Question> Question { get; set; };
    }
}