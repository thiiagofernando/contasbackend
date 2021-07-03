using CadastroConta.Business.Interfaces;
using CadastroConta.Business.Models;
using CadastroConta.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CadastroConta.Data.Repository
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
