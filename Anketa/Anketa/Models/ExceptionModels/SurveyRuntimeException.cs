using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Anketa.Models.ExceptionModels
{
    [Serializable]
    public class SurveyRuntimeException : System.Exception
    {
        public SurveyRuntimeException()
        { }
        public SurveyRuntimeException(string message)
            : base(message)
        { }
        public SurveyRuntimeException(string message, Exception innerException)
            : base(message, innerException)
        { }
        protected SurveyRuntimeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}