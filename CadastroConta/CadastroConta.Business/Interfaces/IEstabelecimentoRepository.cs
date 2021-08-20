using System.Collections.Generic;
using CadastroConta.Business.Models;

namespace CadastroConta.Business.Interfaces
{
    public interface IEstabelecimentoRepository : IRepository<EstabelecimentoModel>
    {
        IEnumerable<EstabelecimentoModel> ListaEstabelecimento(int usuarioid);
    }
}