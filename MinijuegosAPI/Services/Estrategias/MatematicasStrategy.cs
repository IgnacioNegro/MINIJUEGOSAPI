using System;
using System.Text.Json;
using System.Threading.Tasks;
using MinijuegosAPI.Infraestructura.Repositorio;
using MinijuegosAPI.Models.Dtos;
using MinijuegosAPI.Models.Entidades;

namespace MinijuegosAPI.Services.Estrategias
{
    public class MatematicasStrategy : IMinijuegoStrategy
    {
        private readonly IPreguntaRepositorio _preguntaRepositorio;

       public MatematicasStrategy(IPreguntaRepositorio preguntaRepositorio)
        {
            _preguntaRepositorio = preguntaRepositorio;
        }

        public TipoMiniJuego Tipo => TipoMiniJuego.Matematicas;

       public async Task<PreguntaResponse> GenerarAsync()
        {
            int n1 = Random.Shared.Next(1, 101);
            int n2 = Random.Shared.Next(1, 101);
            int n3 = Random.Shared.Next(1, 101);
            int[] numeros = { n1, n2, n3 };
            int suma = n1 + n2 + n3;

            Pregunta pregunta = new Pregunta
            {
                
                Tipo = Tipo,
                Codigo = "SUMA_TRES",
                DatosJson = JsonSerializer.Serialize(new { numeros }),
                RespuestaCorrecta = suma.ToString(),
                FechaCreada = DateTime.Now
            };

            await _preguntaRepositorio.GuardarAsync(pregunta);
            PreguntaResponse response = new PreguntaResponse
            {
                PreguntaId = pregunta.Id,
                Tipo = "matematicas",
                Datos = new { numeros },
                FechaCreacion = pregunta.FechaCreada
            };

            return response;
        }
    }
}
