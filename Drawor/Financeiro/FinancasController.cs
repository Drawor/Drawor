using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Drawor.Financeiro
{
    public class FinancasController : Controller
    {
        // GET: Financas
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CadastroDespesa()
        {
            return null;
        }
    }
}