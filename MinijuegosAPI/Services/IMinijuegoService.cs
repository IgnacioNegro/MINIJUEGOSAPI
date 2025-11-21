using System.Threading.Tasks;
using MinijuegosAPI.Models.Dtos;
using MinijuegosAPI.Models.Entidades;

namespace MinijuegosAPI.Services
{
    public interface IMinijuegoService
    {
        // Genera una nueva pregunta según el tipo de minijuego solicitado
        Task<PreguntaResponse> GenerarPreguntaAsync(TipoMiniJuego tipo);

        // Valida una respuesta enviada por el cliente
        Task<ValidacionResponse?> ValidarRespuestaAsync(ValidarRequest request);
    }
}
