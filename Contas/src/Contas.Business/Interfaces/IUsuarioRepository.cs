using Contas.Business.Models;

namespace Contas.Business.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario ObterUsuarioPorLoginESenha(string username, string senha);
        Usuario GravarNovoUsuario(Usuario usuario);
    }
}
