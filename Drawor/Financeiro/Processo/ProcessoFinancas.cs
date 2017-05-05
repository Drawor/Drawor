﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Drawor.Financeiro.Models;
using Drawor.Financeiro.ViewModels;

namespace Drawor.Financeiro.Processo
{
    public class ProcessoFinancas
    {
        public void CriarTipoDespesa(Models.TipoDespesa tipoDespesa,string currentUserId)
        {
            Mapper.MapperFinanceiro mapper = new Mapper.MapperFinanceiro();
            mapper.CriarTipoDespesa(tipoDespesa,currentUserId);

        }

        internal void CriarConta(Conta dto, string currentUserId)
        {
            Mapper.MapperFinanceiro mapper = new Mapper.MapperFinanceiro();
            mapper.CriarConta(dto, currentUserId);
        }

        internal List<SelectListItem> PegarTodasContasDropDown()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            Mapper.MapperFinanceiro mapper = new Mapper.MapperFinanceiro();
            List<Conta> contas = mapper.PegarTodasContasAtivasDropDownList();

            foreach (var item in contas)
            {
                SelectListItem listItem = new SelectListItem();
                listItem.Value = item.Id.ToString();
                listItem.Text = item.Nome;
                list.Add(listItem);
            }
            return list;
        }

        internal List<BalancoViewModel> GerarBalanco()
        {
            List<BalancoViewModel> balanco = new List<BalancoViewModel>();
            Mapper.MapperFinanceiro mapper = new Mapper.MapperFinanceiro();

            List<Financeiro.ViewModels.DespesaViewModel> despesas = mapper.PegarTodasDespesasPartialView();
            var categorias = mapper.PegarTodosTiposDeDespesaAtivosDropDownList();

            foreach (var item in categorias)
            {
                BalancoViewModel newItem = new BalancoViewModel();

                var id = item.Id.ToString();
                var despesasDaCategoria = (from desCat in despesas
                                           where desCat.TipoDespesa==item.Nome
                                           select desCat).ToList();
                newItem.Total = despesasDaCategoria.Sum(s => s.Valor).ToString();
                newItem.Categoria = item.Nome;

                balanco.Add(newItem);

            }
            return balanco;

        }

        internal void CriarNovaDespesa(DespesaViewModel novaDespesa, string currentUserId)
        {
            Mapper.MapperFinanceiro mapper = new Mapper.MapperFinanceiro();
            mapper.CriarNovaDespesa(novaDespesa, currentUserId);

        }

        internal List<SelectListItem> PegarTodosTiposDespesaDropDownList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            Mapper.MapperFinanceiro mapper = new Mapper.MapperFinanceiro();
            List<TipoDespesa> tiposDespesa = mapper.PegarTodosTiposDeDespesaAtivosDropDownList();

            foreach (var item in tiposDespesa)
            {
                SelectListItem listItem = new SelectListItem();
                listItem.Value = item.Id.ToString();
                listItem.Text = item.Nome;
                list.Add(listItem);
            }
            return list;
        }

        internal List<DespesaViewModel> PegarTodasDespesasViewModel()
        {
            Mapper.MapperFinanceiro mapper = new Mapper.MapperFinanceiro();
            List<DespesaViewModel> Despesas = mapper.PegarTodasDespesasPartialView();
         
            return Despesas;
        }

        internal DespesaViewModel PegarDespesaViewModelPorId(int id)
        {
            Mapper.MapperFinanceiro mapper = new Mapper.MapperFinanceiro();
            return mapper.PegarDespesaViewModelPorId(id);
            
        }

        internal void AtualizarDespesa(DespesaViewModel despesa)
        {
            Mapper.MapperFinanceiro mapper = new Mapper.MapperFinanceiro();
             mapper.UpdateDespesas(despesa);
        }
    }
}