using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
namespace MinijuegosAPI.Models.Dtos
{
    public class ValidarRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int PreguntaId { get; set; }
        
        [Required]
        [MinLength(1)]
        public string Respuesta { get; set; } = string.Empty;
    }
}