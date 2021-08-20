using CadastroConta.Business.Interfaces;
using CadastroConta.Business.Models;
using CadastroConta.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CadastroConta.Data.Repository
{
    public class ContaRepository : Repository<ContaModel>, IContaRepository
    {
        public ContaRepository(ContasDbContext context) : base(context) { }
        
        public IEnumerable<ContaModel> ListarTodasAsContas(int usuarioid)
        {
            return _db.conta
                     .AsNoTracking()
                     .Where(x => x.UsuarioId == usuarioid);
        }
    }
}
