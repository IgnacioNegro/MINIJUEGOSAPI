using System.Diagnostics.CodeAnalysis;
namespace MinijuegosAPI.Models.Dtos

{
    [ExcludeFromCodeCoverage]
    public class PreguntaResponse
    {
        public int PreguntaId { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public object? Datos { get; set; } //ver dps si cambiar segun el tipo
        public DateTime FechaCreacion { get; set; }


    }
}