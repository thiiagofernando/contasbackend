using Contas.Business.Interfaces;
using Contas.Business.Models;
using Contas.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Contas.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ContasDbContext context) : base(context) { }

        public Usuario GravarNovoUsuario(Usuario usuario)
        {
            Db.usuario.Add(usuario);
            var user = Db.usuario.Find(usuario.Id);
            Db.SaveChanges();
            return user;
        }

        public Usuario ObterUsuarioPorLoginESenha(string username, string senha)
        {
            return Db.usuario
               .AsNoTracking()
               .FirstOrDefault(p => p.Login == username && p.Senha == senha);
        }


    }
}
