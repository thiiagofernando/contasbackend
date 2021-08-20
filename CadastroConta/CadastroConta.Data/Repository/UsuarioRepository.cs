using CadastroConta.Business.Interfaces;
using CadastroConta.Business.Models;
using CadastroConta.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CadastroConta.Data.Repository
{
    public class UsuarioRepository : Repository<UsuarioModel>, IUsuarioRepository
    {
        public UsuarioRepository(ContasDbContext context) : base(context) { }

        public UsuarioModel GravarNovoUsuario(UsuarioModel usuarioModel)
        {
            _db.usuario.Add(usuarioModel);
            var user = _db.usuario.Find(usuarioModel.Id);
            _db.SaveChanges();
            return user;
        }

        public UsuarioModel ObterUsuarioPorLogin(string login)
        {
            return _db.usuario.AsNoTracking().FirstOrDefault(p => p.Login == login);
        }

        public UsuarioModel ObterUsuarioPorLoginESenha(string username, string senha)
        {
            return _db.usuario
               .AsNoTracking()
               .FirstOrDefault(p => p.Login == username && p.Senha == senha);
        }


    }
}
