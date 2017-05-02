using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drawor.Financeiro.ViewModels
{
    public class DespesaViewModel
    {
        public bool EstaPago { get; set; }
        public DateTime Vencimento { get; set; }
        public string Descricao { get; set; }
        public String TipoDespesa { get; set; }
        public String Conta { get; set; }
        public double Valor { get; set; }
        public int Id { get; internal set; }
    }
}