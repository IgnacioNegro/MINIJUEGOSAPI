using Microsoft.EntityFrameworkCore;
using MinijuegosAPI.Models.Entidades;
using System.Diagnostics.CodeAnalysis;

namespace MinijuegosAPI.Infraestructura.Data
{
    [ExcludeFromCodeCoverage]
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Pregunta> Preguntas { get; set; }
    }
}