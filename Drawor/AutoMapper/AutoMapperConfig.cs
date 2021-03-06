﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drawor.AutoMapper
{
    public class AutoMapperConfig
    {

        public List<Financeiro.ViewModels.DespesaViewModel> DespesasToView(List<Financeiro.Models.Despesa> despesas)
        {
            List<Financeiro.ViewModels.DespesaViewModel> views = new List<Financeiro.ViewModels.DespesaViewModel>();

            views = (from v in despesas
                     select new Financeiro.ViewModels.DespesaViewModel
                     {
                         TipoDespesa = v.Categoria!=null ? v.Categoria.Nome: null,
                         Conta = v.Conta!=null? v.Conta.Nome:null,
                         Descricao = v.Descricao,
                         EstaPago = v.EstaPago,
                         Valor = v.Valor,
                         Vencimento = v.Vencimento,
                         
                     }).ToList();


            return views;
        }
       
    }
}