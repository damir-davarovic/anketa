﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anketa.Models
{
    public class Answer
    {
        public int ID { get; set; }
        public int questionID { get; set; }
        public string answerText { get; set; }
        public bool correct { get; set; }
    }
}