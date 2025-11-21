using MinijuegosAPI.Infraestructura.Repositorio;
using MinijuegosAPI.Models.Dtos;
using MinijuegosAPI.Models.Entidades;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace MinijuegosAPI.Services.Estrategias
{
    public class LogicaStrategy : IMinijuegoStrategy
    {
        private readonly IPreguntaRepositorio _preguntaRepositorio;

        
        public LogicaStrategy(IPreguntaRepositorio preguntaRepositorio)
        {
            _preguntaRepositorio = preguntaRepositorio;
        }

        public TipoMiniJuego Tipo => TipoMiniJuego.Logica;

        public async Task<PreguntaResponse> GenerarAsync()
        {
            (int n1, int n2, int n3) = (Random.Shared.Next(1, 101), Random.Shared.Next(1, 101), Random.Shared.Next(1, 101));
            int[] numeros = { n1, n2, n3 };

            string[] codigos = { "2PARES", "SUMA_PAR", "MAYOR_SUMA_OTROS", "ALGUNO_MAYOR50", "TODOS_DIFERENTES" };
            string[] proposiciones = {
                "Exactamente 2 números son pares",
                "La suma de los 3 números es par",
                "El número mayor es mayor que la suma de los otros dos",
                "Hay al menos un número mayor que 50",
                "Todos los números son diferentes"
            };

            int indice = Random.Shared.Next(0, codigos.Length);
            string codigo = codigos[indice];
            string texto = proposiciones[indice];

            string respuestaCorrecta = EvaluarProposicion(numeros, codigo) ? "Verdadero" : "Falso";

            Pregunta pregunta = new Pregunta
            {
               
                Tipo = Tipo,
                Codigo = codigo,
                DatosJson = JsonSerializer.Serialize(new { numeros }),
                RespuestaCorrecta = respuestaCorrecta,
                FechaCreada = DateTime.Now
            };

            await _preguntaRepositorio.GuardarAsync(pregunta);

            return new PreguntaResponse
            {
                PreguntaId = pregunta.Id,
                Tipo = "logica",
                Datos = new { numeros, proposicion = texto, codigo_proposicion = codigo },
                FechaCreacion = pregunta.FechaCreada
            };
        }

        //  método privado 
        private bool EvaluarProposicion(int[] numeros, string codigo)
        {
            int pares = numeros.Count(n => n % 2 == 0);
            int suma = numeros.Sum();
            int mayor = numeros.Max();
            int sumaOtros = suma - mayor;
            bool algunoMayor50 = numeros.Any(n => n > 50);
            bool todosDiferentes = numeros.Distinct().Count() == numeros.Length;

            return codigo switch
            {
                "2PARES" => pares == 2,
                "SUMA_PAR" => suma % 2 == 0,
                "MAYOR_SUMA_OTROS" => mayor > sumaOtros,
                "ALGUNO_MAYOR50" => algunoMayor50,
                "TODOS_DIFERENTES" => todosDiferentes,
                _ => false
            };
        }
    }
}
