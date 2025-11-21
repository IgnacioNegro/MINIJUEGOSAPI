using Microsoft.EntityFrameworkCore;
using MinijuegosAPI.Infraestructura.Data;
using MinijuegosAPI.Models.Entidades;

namespace MinijuegosAPI.Infraestructura.Repositorio
{
    public class PreguntaRepositorio : IPreguntaRepositorio
    {
        private readonly ApplicationDbContext _db;
        //Inyecto db context a trabes de la dependencia
        public PreguntaRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }

        // Traigo la pregunta
        public async Task<Pregunta?> ObtenerPorIdAsync(int id)
        {
            return await _db.Preguntas.FirstOrDefaultAsync(p => p.Id == id);
        }

       //La guardo
        public async Task GuardarAsync(Pregunta pregunta)
        {
            _db.Preguntas.Add(pregunta);
            await _db.SaveChangesAsync();
        }
    }
}
