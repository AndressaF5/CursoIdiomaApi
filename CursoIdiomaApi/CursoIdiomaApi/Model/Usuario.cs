using System.ComponentModel.DataAnnotations;

namespace CursoIdiomaApi.Model
{
    public class Usuario
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}
