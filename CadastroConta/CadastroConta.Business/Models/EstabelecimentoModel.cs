namespace CadastroConta.Business.Models
{
    public class EstabelecimentoModel : Entity
    {
        public string Descricao { get; set; }
        public int UsuarioId { get; set; }
        public  UsuarioModel Usuario { get; set; }
    }
}