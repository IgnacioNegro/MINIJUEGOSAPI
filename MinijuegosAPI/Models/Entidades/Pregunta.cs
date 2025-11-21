using System.Text.Json.Serialization;
using System.Diagnostics.CodeAnalysis;
namespace MinijuegosAPI.Models.Entidades
{
    [ExcludeFromCodeCoverage]

    public class Pregunta
    {
        public int Id { get; set; }

        public TipoMiniJuego Tipo { get; set; }

        public string DatosJson { get; set; } = string.Empty;

        public string RespuestaCorrecta { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;

        public DateTime FechaCreada { get; set; } = DateTime.Now;

     
    }

    [JsonConverter(typeof(JsonStringEnumConverter))] //Para que aparezca el nombre y no el numero en Swagger
    public enum TipoMiniJuego
    {
        Matematicas = 1,
        Memoria = 2,
        Logica = 3
    }

}