using System.ComponentModel.DataAnnotations;

namespace CadastroConta.Api.ViewModels
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = "Este campo é Obrigatório")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter entre 3 e 100 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Este campo é Obrigatório")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter entre 3 e 100 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 100 caracteres")]
        public string Senha { get; set; }
    }
}
