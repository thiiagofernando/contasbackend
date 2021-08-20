using System.ComponentModel.DataAnnotations;

namespace CadastroConta.Api.ViewModels
{
    public class EstabelecimentoDto
    {
        public  int Id { get; set; }
        [Required(ErrorMessage = "Este campo Nome é Obrigatório")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter entre 3 e 100 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 100 caracteres")]
        public  string Descricao { get; set; }
        public  int UsuarioId { get; set; }
    }
}