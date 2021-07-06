using CadastroConta.Business.Models;
using System;
using System.Collections.Generic;

namespace CadastroConta.Business.Interfaces
{
    public interface IContaRepository : IRepository<Conta>
    {
        int CalcularDiasEmAtraso(DateTime dataVencimento, DateTime? dataPagamento);
        decimal CalcularMultaEJuros(int diasEmAtraso, decimal valorOriginal);
        IEnumerable<Conta> ListarTodasAsContas();
    }
}
