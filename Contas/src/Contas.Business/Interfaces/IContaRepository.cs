using Contas.Business.Models;
using System;
using System.Collections.Generic;

namespace Contas.Business.Interfaces
{
    public interface IContaRepository : IRepository<Conta>
    {
        int CalcularDiasEmAtraso(DateTime dataVencimento, DateTime dataPagamento);
        decimal CalcularMultaEJuros(int diasEmAtraso, decimal valorOriginal);
        IEnumerable<Conta> ListarTodasAsContas();
    }
}
