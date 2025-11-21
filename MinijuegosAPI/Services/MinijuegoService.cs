using Microsoft.EntityFrameworkCore;
using MinijuegosAPI.Infraestructura.Repositorio;
using MinijuegosAPI.Models.Dtos;
using MinijuegosAPI.Models.Entidades;
using MinijuegosAPI.Services.Estrategias;
using MinijuegosAPI.Services.Factories;

namespace MinijuegosAPI.Services
{
    public class MinijuegoService : IMinijuegoService
    {
        private readonly IMinijuegoFactory _factory; 
        private readonly IPreguntaRepositorio _preguntaRepositorio; //Repository patronnn, inyeccion dependencia

        public MinijuegoService(IMinijuegoFactory factory, IPreguntaRepositorio preguntaRepositorio)
        {
            _factory = factory;
            _preguntaRepositorio = preguntaRepositorio;
        }
        // Generar Preguntas conn Patron strategy
        public async Task<PreguntaResponse> GenerarPreguntaAsync(TipoMiniJuego tipo)
        {
            IMinijuegoStrategy estrategia = _factory.CrearEstrategia(tipo);
            if (estrategia == null) throw new InvalidOperationException("Estrategia no encontrada para el tipo que solicito.");

            return await estrategia.GenerarAsync();
        }

        //Validar las respuestas
        public async Task<ValidacionResponse?> ValidarRespuestaAsync(ValidarRequest request)
        {
            Pregunta? pregunta = await _preguntaRepositorio.ObtenerPorIdAsync(request.PreguntaId);

            if (pregunta == null)
            {return null;}

            bool esCorrecta = string.Equals(
                pregunta.RespuestaCorrecta?.Trim(),
                request.Respuesta?.Trim(),
                StringComparison.OrdinalIgnoreCase
            );

            return new ValidacionResponse
            {
                EsCorrecta = esCorrecta,
                Mensaje = esCorrecta ? "¡Respuesta correcta!" : "Respuesta incorrecta.",
                RespuestaCorrecta = pregunta.RespuestaCorrecta ?? string.Empty,
                TipoMinijuego = pregunta.Tipo.ToString().ToLower()
            };
        }
    }
}
