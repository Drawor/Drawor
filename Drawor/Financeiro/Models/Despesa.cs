using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drawor.Financeiro.Models
{
    public class Despesa
    {
        public bool EstaPago { get; set; }
        public DateTime Vencimento { get; set; }
        public string Descricao { get; set; }
        public TipoDespesa Categoria { get; set; }
        public Conta Conta { get; set; }
        public double Valor { get; set; }
            }
}