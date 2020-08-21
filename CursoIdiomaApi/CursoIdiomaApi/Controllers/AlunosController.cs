using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CursoIdiomaApi.Data;
using CursoIdiomaApi.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CursoIdiomaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        ApplicationDbContext context = new ApplicationDbContext();

        // GET: api/<AlunosController>
        [HttpGet]
        public IEnumerable<Aluno> Get()
        {
            string connectionString = "Server=DESKTOP-OCEO2U4;Database=cursoIdioma;Trusted_Connection=True;";

            var alunos = new List<Aluno>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var query = "SELECT * FROM dbo.Alunos";
                SqlCommand select = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    using (var reader = select.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            var aluno = new Aluno();
                            aluno.Id = (int)reader["Id"];
                            aluno.Nome = reader["Nome"].ToString();
                            aluno.Matricula = (int)reader["Matricula"];
                            aluno.TurmaId = (int)reader["TurmaId"];

                            alunos.Add(aluno);
                        }
                    }
                }catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    connection.Close();
                }
            }

            return alunos;
        }

        // GET api/<AlunosController>/9
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            if (id == null)
            {
                return BadRequest();
            }
            var aluno = context.Alunos.Find(id);
            if (aluno == null)
            {
                return NotFound("Aluno não encontrado");
            }

            return Ok(aluno);
        }

        // POST api/<AlunosController>
        [HttpPost]
        public void Post([FromBody] Aluno aluno)
        {
            context.Alunos.Add(aluno);
            context.SaveChanges();
        }

        // PUT api/<AlunosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AlunosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var aluno = context.Alunos.Find(id);
            context.Alunos.Remove(aluno);
            context.SaveChanges();
        }
    }
}
