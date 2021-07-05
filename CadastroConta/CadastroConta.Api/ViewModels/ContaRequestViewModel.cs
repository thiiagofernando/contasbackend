﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CadastroConta.Api.ViewModels
{
    public class ContaRequestViewModel
    {
        public Guid? Id { get; set; }
        [Required(ErrorMessage = "Este campo Nome é Obrigatório")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter entre 3 e 100 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo Valor é Obrigatório")]
        public decimal ValorOriginal { get; set; }

        [Required(ErrorMessage = "Este campo Data de Vencimento é Obrigatório")]
        public DateTime DataVencimento { get; set; }

        [Required(ErrorMessage = "Este campo Data de Pagamento é Obrigatório")]
        public DateTime DataPagamento { get; set; }
        public int PagamentoRealizado { get; set; }
    }
}
