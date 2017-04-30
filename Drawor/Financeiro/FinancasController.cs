using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
            return View("CadastrarDespesa");
        }
        public ActionResult CadastroTipoDespesa()
        {
            return View("CadastroTipoDespesa");
        }
        public ActionResult NovoTipoDespesa(Financeiro.Models.TipoDespesa dto)
        {
            var currentUserId = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(HttpContext.User.Identity.GetUserId()).Id;
            Processo.ProcessoFinancas processo = new Processo.ProcessoFinancas();
            processo.CriarTipoDespesa(dto, currentUserId);
            return null;
        }

        
    }
}