using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Drawor.Financeiro
{
    public class DropDownListFinanceiroController : Controller
    {
        // GET: DropDownListFinanceiro
        public ActionResult Index()
        {

            return View();
        }
        public JsonResult DropDownListContas()
        {
             
            Processo.ProcessoFinancas process = new Processo.ProcessoFinancas();

            List<SelectListItem> myList = process.PegarTodasContasDropDown();

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