namespace CadastroConta.Business.Models
{
    public class Usuario : Entity
    {
        public string Login { get; set; }
        public string NomeCompleto { get; set; }
        public string Senha { get; set; }
    }
}
