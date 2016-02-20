using Anketa.App_Start;
using Anketa.DAL.AnswersDAL;
using Anketa.Models.AjaxModels;
using Anketa.Models.AnswerModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;

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

        public JsonResult _AjaxAddChoiceItemSingle()
        {
            AnswerChoiceSingle templateChoiceItem = aService.fetchTemplateChoiceItemSingle();
            try
            {
                string renderedHtml = UtilitiesClass.RenderViewToString(this.ControllerContext, "~/Views/Answer/Partials/_SinglePartial.cshtml", templateChoiceItem);
                ajaxResponse.message = renderedHtml;
                return Json(ajaxResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                ajaxResponse.message = "Something went wrong! " + e.StackTrace;
                ajaxResponse.type = 0;
                return Json(ajaxResponse, JsonRequestBehavior.AllowGet);
            }            
        }
    }


}