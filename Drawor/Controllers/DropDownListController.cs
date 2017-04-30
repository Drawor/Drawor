using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Drawor.Controllers
{
    public class DropDownListController : Controller
    {
        // GET: DropDownList
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult DropDownListContas()
        {
            List<SelectListItem> myList = new List<SelectListItem>();
            return Json(myList, JsonRequestBehavior.AllowGet);

        }
        public JsonResult DropDownListTipoDespesa()
        {
            List<SelectListItem> myList = new List<SelectListItem>();
                     //new SelectListItem{ Value="1",Text="Open/In Transit"},        
            return Json(myList, JsonRequestBehavior.AllowGet);
        }
    }
}