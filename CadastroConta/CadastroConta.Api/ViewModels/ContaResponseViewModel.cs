using System;

namespace CadastroConta.Api.ViewModels
{
    public class ContaResponseViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string ValorOriginal { get; set; }
        public string ValorCorrigido { get; set; }
        public int DiasEmAtraso { get; set; }
        public string DataPagamento { get; set; }
        public string DataVencimento { get; set; }
        public string PagamentoRealizado { get; set; }
    }
}
