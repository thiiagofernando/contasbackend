namespace CadastroConta.Business.Models
{
    public class UsuarioModel : Entity
    {
        public string Login { get; set; }
        public string NomeCompleto { get; set; }
        public string Senha { get; set; }
    }
}
