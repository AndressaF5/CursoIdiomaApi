using System.ComponentModel.DataAnnotations;

namespace CursoIdiomaApi.Model
{
    public class Usuario
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Senha deve conter no mínimo 6 caracteres")]
        [MaxLength(16, ErrorMessage = "Senha deve conter no máximo 16 caracteres")]
        public string Senha { get; set; }
    }
}
