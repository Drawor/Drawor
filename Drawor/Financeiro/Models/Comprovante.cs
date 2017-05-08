using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drawor.Financeiro.Models
{
    public class Comprovante
    {
        public string String64 { get;  set; }
        public string ContentType { get;  set; }
        public string NomeArquito { get;  set; }
        public byte[] Bytes { get; internal set; }
        public string Type { get; set; }
    }
}