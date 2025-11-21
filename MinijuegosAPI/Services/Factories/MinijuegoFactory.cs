using MinijuegosAPI.Models.Entidades;
using MinijuegosAPI.Services.Estrategias;

namespace MinijuegosAPI.Services.Factories
{
    public class MinijuegoFactory:IMinijuegoFactory
    {
        private readonly IEnumerable<IMinijuegoStrategy> _estrategias;

        public MinijuegoFactory(IEnumerable<IMinijuegoStrategy> estrategias)
        {
            _estrategias = estrategias;
        }

        public IMinijuegoStrategy CrearEstrategia(TipoMiniJuego tipo)
        {
            IMinijuegoStrategy? estrategia = _estrategias.FirstOrDefault(e => e.Tipo == tipo);
            if (estrategia == null) throw new NotSupportedException($"Tipo de minijuego '{tipo}' no implementado.");

            return estrategia;
        }
    }
}
