﻿using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
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
        public ActionResult Despesas()
        {
            return View("Despesas");
        }
        public ActionResult CadastroDespesa()
        {
            return View("CadastrarDespesa");
        }
        public ActionResult Balanco()
        {
            
            return View("Balanco");
        }
        public ActionResult NovaDespesa(ViewModels.DespesaViewModel novaDespesa)

        {

            var currentUserId = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(HttpContext.User.Identity.GetUserId()).Id;
            Financeiro.Processo.ProcessoFinancas processo = new Processo.ProcessoFinancas();

            processo.CriarNovaDespesa(novaDespesa, currentUserId);


            return View("CadastrarDespesa");
        }
        public ActionResult CadastroTipoDespesa()
        {
            return View("CadastroTipoDespesa");
        }
        public JsonResult NovoTipoDespesa(Financeiro.Models.TipoDespesa dto)
        {
            var currentUserId = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(HttpContext.User.Identity.GetUserId()).Id;
            Processo.ProcessoFinancas processo = new Processo.ProcessoFinancas();
            processo.CriarTipoDespesa(dto, currentUserId);

            return Json(true);
        }

        public ActionResult CadastroConta()
        {
            return View("CadastroConta");
        }
        public JsonResult NovaConta(Models.Conta dto)
        {
            var currentUserId = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(HttpContext.User.Identity.GetUserId()).Id;
            Processo.ProcessoFinancas processo = new Processo.ProcessoFinancas();
            processo.CriarConta(dto, currentUserId);
            return Json(true);
        }

        public ActionResult TodasDespesas([DataSourceRequest] DataSourceRequest request)
        {
            List<Financeiro.ViewModels.DespesaViewModel> despesas = new List<ViewModels.DespesaViewModel>();

            Financeiro.Processo.ProcessoFinancas processo = new Processo.ProcessoFinancas();
            despesas = processo.PegarTodasDespesasViewModel();
            
            return Json(despesas.ToDataSourceResult(request));
           
        }
        public ActionResult EditarDespesa(int? Id)
        {

            Financeiro.ViewModels.DespesaViewModel despesa = new ViewModels.DespesaViewModel();
            Financeiro.Processo.ProcessoFinancas processo = new Processo.ProcessoFinancas();
            despesa = processo.PegarDespesaViewModelPorId((int)Id);
            despesa.Id = (int)Id;
            ViewBag.Id = Id.ToString();
            return View("EditarDespesa", despesa);
            
        }
        public ActionResult UpdateDespesa(Financeiro.ViewModels.DespesaViewModel despesa, string myHiddenInput, string Id)
        { 
            Financeiro.Processo.ProcessoFinancas processo = new Processo.ProcessoFinancas();
           processo.AtualizarDespesa(despesa);

            return View("Despesas");
        }

        public ActionResult GerarBalanco()
        {
            Processo.ProcessoFinancas processo = new Processo.ProcessoFinancas();
            List<Financeiro.ViewModels.BalancoViewModel> balancos = processo.GerarBalanco();

            return Json(balancos);
            
        }
        public ActionResult UploadComprovante(IEnumerable<HttpPostedFileBase> Comprovante)
        {
            var currentUserId = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(HttpContext.User.Identity.GetUserId()).Id;
            Processo.ProcessoFinancas processo = new Processo.ProcessoFinancas();
           
            List<string> ids = new List<string>();
            foreach (var item in Comprovante)
            {
                processo.InserirComprovante(item, currentUserId);
                ids.AddRange( processo.PegarUltimoIdArquivos());
            }

            return Json(ids.FirstOrDefault());
        }
        public ActionResult PegarComprovanteById(int? Id)
        {
            Processo.ProcessoFinancas processo = new Processo.ProcessoFinancas();
            var comprovante = processo.PegarComprovantebyId((int)Id);

            return File(comprovante.Bytes, comprovante.ContentType);
        }
    }
}