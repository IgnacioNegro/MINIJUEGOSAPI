using System.Diagnostics.CodeAnalysis;

namespace MinijuegosAPI.Models.Dtos
{
    [ExcludeFromCodeCoverage]
    public class ValidacionResponse
    {
        public bool EsCorrecta { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public string RespuestaCorrecta { get; set; } = string.Empty;
        public string TipoMinijuego { get; set; } = string.Empty;
    }
}
