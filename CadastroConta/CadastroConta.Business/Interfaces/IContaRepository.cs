using CadastroConta.Business.Models;
using System;
using System.Collections.Generic;

namespace CadastroConta.Business.Interfaces
{
    public interface IContaRepository : IRepository<ContaModel>
    {
        IEnumerable<ContaModel> ListarTodasAsContas(int usuarioId);
    }
}
