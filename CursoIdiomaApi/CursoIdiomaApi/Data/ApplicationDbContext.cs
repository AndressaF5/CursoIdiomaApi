using CursoIdiomaApi.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CursoIdiomaApi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Turma> Turmas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-OCEO2U4;Database=cursoIdioma;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Aluno>()
                .HasOne(t => t.Turma)
                .WithMany(a => a.Alunos)
                .HasForeignKey(t => t.TurmaId);

            builder.Entity<Turma>()
                .HasMany(a => a.Alunos)
                .WithOne(a => a.Turma);
        }
    }
}
