using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        
    }
}