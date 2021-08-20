using System;

namespace CadastroConta.Business.Models
{
    public class ContaModel : Entity
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime? DataPagamento { get; set; }
        public bool PagamentoRealizado { get; set; }
        public  int EstabelecimentoId { get; set; }
        public  int UsuarioId { get; set; }
        public  EstabelecimentoModel Estabelecimento { get; set; }
        public  UsuarioModel Usuario { get; set; }
    }
}
