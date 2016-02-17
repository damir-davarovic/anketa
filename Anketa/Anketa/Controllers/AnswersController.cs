using Anketa.DAL.AnswersDAL;
using Anketa.Models.AjaxModels;
using Anketa.Models.AnswerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Anketa.Controllers
{
    public class AnswersController : Controller
    {
        private AnswersService aService = new AnswersService();
        private _AjaxResponseModel ajaxResponse = new _AjaxResponseModel();

        [HttpPost]
        public JsonResult _AjaxDeleteSingleChoice(AnswerChoiceSingle pChoiceItem)
        {
            try
            {
                ajaxResponse = aService._AjaxDeleteSingleChoice(pChoiceItem);
                return Json(ajaxResponse, JsonRequestBehavior.AllowGet);   
            }
            catch (Exception e)
            {
                ajaxResponse.message = "Database action failed! " + e.StackTrace;
                ajaxResponse.type = 0;
                return Json(ajaxResponse, JsonRequestBehavior.AllowGet);
            }
        }
    }
}