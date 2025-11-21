using MinijuegosAPI.Models.Entidades;
using MinijuegosAPI.Services.Estrategias;

namespace MinijuegosAPI.Services.Factories
{
    public interface IMinijuegoFactory
    {
        IMinijuegoStrategy CrearEstrategia(TipoMiniJuego tipo);
    }
}