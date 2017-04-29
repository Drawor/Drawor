using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Drawor.Financeiro
{
    public class FinanceiroController : Controller
    {
        // GET: Financeiro
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FluxoCaixa ()
        {
            return View("FluxoCaixa");
        }
    }
}