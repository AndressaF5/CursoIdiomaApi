using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoIdiomaApi.Model
{
    public class Aluno
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Matrícula é obrigatório")]
        [JsonRequired]
        public int Matricula { get; set; }
        [Required(ErrorMessage = "Turma é obrigatório")]
        public int TurmaId { get; set; }
        [JsonIgnore]
        public Turma Turma { get; set; }
    }
}
