using CadastroConta.Business.Models;

namespace CadastroConta.Business.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario ObterUsuarioPorLoginESenha(string username, string senha);
        Usuario GravarNovoUsuario(Usuario usuario);
    }
}
