using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anketa.Models
{
    public enum TipPitanja
    {
        single, multiple
    }

    public class Question
    {
        public int ID { get; set; }
        public int surveyID { get; set; }
        public TipPitanja? TipPitanja { get; set; }
        public bool aktivnoPitanje { get; set; }
    }
}