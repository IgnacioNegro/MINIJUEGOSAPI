using System.Threading.Tasks;
using MinijuegosAPI.Models.Dtos;
using MinijuegosAPI.Models.Entidades;

namespace MinijuegosAPI.Services.Estrategias
{
    public interface IMinijuegoStrategy
    {
        TipoMiniJuego Tipo { get; }
        Task<PreguntaResponse> GenerarAsync();
    }
}