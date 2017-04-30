﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Drawor.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Financeiro.Models.Despesa var = new Financeiro.Models.Despesa();

            var.Categoria = new Financeiro.Models.TipoDespesa { Cor = "Cor", Nome = "Nome" };
            var.Conta = new Financeiro.Models.Conta {Nome = "Nome", SaldoAtual= 15.15 };
            var.Descricao = "Descricao";
            var.EstaPago = true;
            var.Valor = 15.16;
            var.Vencimento = DateTime.Now;
            
            var test = Mapper.Map<Financeiro.ViewModels.DespesaViewModel>(var);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}