using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anketa.Models.AjaxModels
{
    public class _AjaxResponseModel
    {
        public int type { get; set; } // 0 - All OK; 1 - Some kind of error; 2 - Validation failed;
        public string message { get; set; }
        public int questionId {get; set; }
        public int surveyId { get; set; }
        public int answerId { get; set; }
        public string stringData { get; set; }
    }
}