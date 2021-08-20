using System;
using System.ComponentModel.DataAnnotations;

namespace CadastroConta.Api.ViewModels
{
    public class ContaDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo Nome é Obrigatório")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter entre 3 e 100 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 100 caracteres")]
        public string Descricao { get; set; }

        public string DescricaoEstabelecimento { get; set; }

        [Required(ErrorMessage = "Este campo Valor é Obrigatório")]
        public decimal Valor { get; set; }

        public DateTime? DataPagamento { get; set; }
        [Required(ErrorMessage = "Este campo Data de Pagamento é Obrigatório")]
        public bool PagamentoRealizado { get; set; }
        public  int UsuarioId { get; set; }
        public  int EstabelecimentoId { get; set; }
    }
}
