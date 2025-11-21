using MinijuegosAPI.Models.Entidades;

namespace MinijuegosAPI.Infraestructura.Repositorio
{
    public interface IPreguntaRepositorio
    {
        Task<Pregunta?> ObtenerPorIdAsync(int id);
        Task GuardarAsync(Pregunta pregunta);
    }
}