using System.Collections.Generic;
using System.Linq;
using CadastroConta.Business.Interfaces;
using CadastroConta.Business.Models;
using CadastroConta.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CadastroConta.Data.Repository
{
    public class EstabelecimentoRepository :Repository<EstabelecimentoModel>, IEstabelecimentoRepository
    {
        public EstabelecimentoRepository(ContasDbContext context) : base(context)
        {
        }

        public IEnumerable<EstabelecimentoModel> ListaEstabelecimento(int usuarioid)
        {
            return _db.estabelecimento.AsNoTracking()
                .Where(c => c.UsuarioId == usuarioid);
        }
    }
}