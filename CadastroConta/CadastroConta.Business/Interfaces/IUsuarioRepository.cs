using CadastroConta.Business.Models;

namespace CadastroConta.Business.Interfaces
{
    public interface IUsuarioRepository : IRepository<UsuarioModel>
    {
        UsuarioModel ObterUsuarioPorLoginESenha(string username, string senha);
        UsuarioModel GravarNovoUsuario(UsuarioModel usuarioModel);
        UsuarioModel ObterUsuarioPorLogin(string login);
    }
}
