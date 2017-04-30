using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drawor.Financeiro.Models
{
    public class Conta
    {
        public string Nome { get; set; }
        public double SaldoAtual { get; set; }
        public object Id { get; internal set; }
        public string Obsoleto { get; internal set; }
        public string CreateBy { get; internal set; }
        public string CreateTime { get; internal set; }
    }
}