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
            Processo.ProcessoFinancas process = new Processo.ProcessoFinancas();

            List<SelectListItem> myList = process.PegarTodosTiposDespesaDropDownList();
            return Json(myList, JsonRequestBehavior.AllowGet);
        }
    }
}