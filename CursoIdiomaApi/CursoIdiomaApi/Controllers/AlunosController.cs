using CursoIdiomaApi.Data;
using CursoIdiomaApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CursoIdiomaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AlunosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost]
        public ActionResult Cadastrar([FromBody] Aluno aluno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var alunosSalvos = _context.Alunos;

            foreach (var propriedade in alunosSalvos)
            {
                if (aluno.Matricula == propriedade.Matricula)
                {
                    return BadRequest("Número de matrícula está sendo usado");
                }
            }

            _context.Alunos.Add(aluno);
            _context.SaveChanges();

            return Created($"/api/alunos/{aluno.Id}", aluno);

        }

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var aluno = _context.Alunos.Find(id);

            if (aluno == null)
            {
                return NotFound();
            }

            if (aluno.TurmaId != null)
            {
                return BadRequest("Aluno está em uma turma e não pode ser excluído");
            }

            _context.Alunos.Remove(aluno);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
