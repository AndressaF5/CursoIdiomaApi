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
    public class TurmasController : ControllerBase
    {

        ApplicationDbContext context = new ApplicationDbContext();

        // GET: api/<TurmasController>
        [HttpGet]
        public IEnumerable<Turma> Get()
        {
            string connectionString = "Server=DESKTOP-OCEO2U4;Database=cursoIdioma;Trusted_Connection=True;";

            var turmas = new List<Turma>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var query = "SELECT * FROM dbo.Turmas";
                SqlCommand select = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    using (var reader = select.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            var turma = new Turma();
                            turma.Id = (int)reader["Id"];
                            turma.Nome = reader["Nome"].ToString();

                            turmas.Add(turma);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    connection.Close();
                }
            }

            return turmas;

        }

        // GET api/<TurmasController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var turma = context.Turmas.Find(id);
            if(turma == null)
            {
                return NotFound("Turma não existe");
            }
            else
            {
                return Ok(turma);
            }
        }

        // POST api/<TurmasController>
        [HttpPost]
        public IActionResult Post([FromBody] Turma turma)
        {

            context.Turmas.Add(turma);
            context.SaveChanges();

            return Ok(turma); // TODO: Trocar para Created
        }

        // PUT api/<TurmasController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TurmasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var turma = context.Turmas.Find(id);
            context.Turmas.Remove(turma);
            context.SaveChanges();
        }
    }
}
