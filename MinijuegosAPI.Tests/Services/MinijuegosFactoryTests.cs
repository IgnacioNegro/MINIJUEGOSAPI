using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using MinijuegosAPI.Services.Factories;
using MinijuegosAPI.Services.Estrategias;
using MinijuegosAPI.Models.Entidades;

namespace MinijuegosAPI.Tests.Services
{
    public class MinijuegoFactoryTests
    {
        [Fact]
        public void DebeCrearEstrategia_CuandoExiste()
        {
            Mock<IMinijuegoStrategy> matematicas = new Mock<IMinijuegoStrategy>();
            matematicas.SetupGet(s => s.Tipo).Returns(TipoMiniJuego.Matematicas);

            List<IMinijuegoStrategy> lista = new List<IMinijuegoStrategy> { matematicas.Object };
            MinijuegoFactory factory = new MinijuegoFactory(lista);

            IMinijuegoStrategy estrategia = factory.CrearEstrategia(TipoMiniJuego.Matematicas);

            Assert.NotNull(estrategia);
        }

        [Fact]
        public void DebeLanzarExcepcion_SiNoExisteTipo()
        {
            List<IMinijuegoStrategy> lista = new List<IMinijuegoStrategy>();
            MinijuegoFactory factory = new MinijuegoFactory(lista);

            Action accion = () => factory.CrearEstrategia(TipoMiniJuego.Memoria);

            Assert.Throws<NotSupportedException>(accion);
        }
    }
}
