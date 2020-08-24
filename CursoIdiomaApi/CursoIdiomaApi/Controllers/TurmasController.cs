using CursoIdiomaApi.Data;
using CursoIdiomaApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CursoIdiomaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TurmasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost]
        public ActionResult Post([FromBody] Turma turma)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Turmas.Add(turma);
            _context.SaveChanges();

            return Ok(turma);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var turma = _context.Turmas.Find(id);

            if (turma == null)
            {
                return BadRequest();
            }

            _context.Turmas.Remove(turma);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
