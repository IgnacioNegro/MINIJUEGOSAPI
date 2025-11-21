using MinijuegosAPI.Infraestructura.Repositorio;
using MinijuegosAPI.Models.Dtos;
using MinijuegosAPI.Models.Entidades;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace MinijuegosAPI.Services.Estrategias
{
    public class MemoriaStrategy : IMinijuegoStrategy
    {
        private readonly IPreguntaRepositorio _preguntaRepositorio;

        public MemoriaStrategy(IPreguntaRepositorio preguntaRepositorio)
        {
            _preguntaRepositorio = preguntaRepositorio;
        }

        public TipoMiniJuego Tipo => TipoMiniJuego.Memoria;

        public async Task<PreguntaResponse> GenerarAsync()
        {
            int[] secuencia = new int[5];
            for (int i = 0; i < 5; i++)
                secuencia[i] = Random.Shared.Next(1, 21);

            string[] codigos = { "2PARES", "2IMPARES", "SUMATODOMAYOR50", "2IGUALES", "ALGUNO_MENOR10" };
            string[] textos = {
                "¿Había exactamente 2 números pares?",
                "¿Había exactamente 2 números impares?",
                "¿La suma de todos los números superaba 50?",
                "¿Había 2 números iguales?",
                "¿Había algún número menor a 10?"
            };

            int indice = Random.Shared.Next(0, codigos.Length);
            string codigo = codigos[indice];
            string texto = textos[indice];

            string respuestaCorrecta = CalcularRespuestaMemoria(secuencia, codigo) ? "Sí" : "No";

            Pregunta? pregunta = new Pregunta
            {
               Tipo = Tipo,
                Codigo = codigo,
                DatosJson = JsonSerializer.Serialize(new { secuencia }),
                RespuestaCorrecta = respuestaCorrecta,
                FechaCreada = DateTime.Now
            };

            await _preguntaRepositorio.GuardarAsync(pregunta);

            return new PreguntaResponse
            {
                PreguntaId = pregunta.Id,
                Tipo = "memoria",
                Datos = new { secuencia, pregunta = texto, codigo_pregunta = codigo },
                FechaCreacion = pregunta.FechaCreada
            };
        }

        // método privado
        private bool CalcularRespuestaMemoria(int[] secuencia, string codigo)
        {
            int pares = secuencia.Count(n => n % 2 == 0);
            int impares = secuencia.Length - pares;
            int suma = secuencia.Sum();
            bool hayIguales = secuencia.Distinct().Count() != secuencia.Length;
            bool hayMenor10 = secuencia.Any(n => n < 10);

            return codigo switch
            {
                "2PARES" => pares == 2,
                "2IMPARES" => impares == 2,
                "SUMATODOMAYOR50" => suma > 50,
                "2IGUALES" => hayIguales,
                "ALGUNO_MENOR10" => hayMenor10,
                _ => false
            };
        }
    }
}
