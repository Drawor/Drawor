﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Drawor.Financeiro.Models;

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

      
    }
}