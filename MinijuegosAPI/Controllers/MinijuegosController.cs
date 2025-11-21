using Microsoft.AspNetCore.Mvc;
using MinijuegosAPI.Models.Dtos;
using MinijuegosAPI.Models.Entidades;
using MinijuegosAPI.Services;
using System.ComponentModel.DataAnnotations;

namespace MinijuegosAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MinijuegosController : ControllerBase
    {
        private readonly IMinijuegoService _service;


        public MinijuegosController(IMinijuegoService service)
        {
            _service = service;
        }


        /// <summary>
        /// Genera una nueva pregunta para el minijuego indicado.
        /// </summary>
        
        [HttpGet("pregunta")]
        public async Task<IActionResult> ObtenerPregunta([FromQuery][Required] TipoMiniJuego tipo)
        {
            PreguntaResponse pregunta = await _service.GenerarPreguntaAsync(tipo);
            if (pregunta is null) return NotFound("No se pudo generar la pregunta");
            return Ok(pregunta);
        }


        /// <summary>
        /// Para matematica, se espera el resultado de la suma. Para Lógica, Verdadero Falso, y para Memoria, Si / No
        /// </summary>
        [HttpPost("validar")]
        public async Task<IActionResult> ValidarRespuesta([FromBody][Required] ValidarRequest request)
        {
            if (request is null)return BadRequest("Datos inválidos");
            ValidacionResponse respuesta = await _service.ValidarRespuestaAsync(request);
            if (respuesta == null) return NotFound("Pregunta no encontrada");
        
            return Ok(respuesta);
        }
    }
}