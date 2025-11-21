using MinijuegosAPI.Infraestructura.Repositorio;
using MinijuegosAPI.Models.Dtos;
using MinijuegosAPI.Models.Entidades;
using MinijuegosAPI.Services;
using MinijuegosAPI.Services.Estrategias;
using MinijuegosAPI.Services.Factories;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MinijuegosAPI.Tests.Services
{
    public class MinijuegoServiceTests
    {
        private readonly Mock<IMinijuegoFactory> _factoryMock;
        private readonly Mock<IPreguntaRepositorio> _repoMock;
        private readonly MinijuegoService _service;

        public MinijuegoServiceTests()
        {
            _factoryMock = new Mock<IMinijuegoFactory>();
            _repoMock = new Mock<IPreguntaRepositorio>();
            _service = new MinijuegoService(_factoryMock.Object, _repoMock.Object);
        }

        [Fact]
        public async Task DebeGenerarPregunta_Matematicas()
        {
            Mock<IMinijuegoStrategy> strategyMock = new Mock<IMinijuegoStrategy>();
            PreguntaResponse respuesta = new PreguntaResponse { PreguntaId = 1, Tipo = "matematicas" };
            strategyMock.Setup(s => s.GenerarAsync()).ReturnsAsync(respuesta);

            _factoryMock
                .Setup(f => f.CrearEstrategia(TipoMiniJuego.Matematicas))
                .Returns(strategyMock.Object);

            PreguntaResponse resultado = await _service.GenerarPreguntaAsync(TipoMiniJuego.Matematicas);

            Assert.NotNull(resultado);
            Assert.Equal("matematicas", resultado.Tipo);
            _factoryMock.Verify(f => f.CrearEstrategia(TipoMiniJuego.Matematicas), Times.Once);
            strategyMock.Verify(s => s.GenerarAsync(), Times.Once);
        }

        [Fact]
        public async Task DebeValidar_Correcta()
        {
            Pregunta pregunta = new Pregunta { Id = 1, RespuestaCorrecta = "135", Tipo = TipoMiniJuego.Matematicas };
            _repoMock.Setup(r => r.ObtenerPorIdAsync(1)).ReturnsAsync(pregunta);

            ValidarRequest request = new ValidarRequest { PreguntaId = 1, Respuesta = "135" };

            ValidacionResponse? resultado = await _service.ValidarRespuestaAsync(request);

            Assert.NotNull(resultado);
            Assert.True(resultado!.EsCorrecta);
            Assert.Equal("¡Respuesta correcta!", resultado.Mensaje);
            Assert.Equal("matematicas", resultado.TipoMinijuego);
        }

        [Fact]
        public async Task DebeRetornarNull_SiNoExistePregunta()
        {
            _repoMock.Setup(r => r.ObtenerPorIdAsync(99)).ReturnsAsync((Pregunta?)null);

            ValidarRequest request = new ValidarRequest { PreguntaId = 99, Respuesta = "x" };

            ValidacionResponse? resultado = await _service.ValidarRespuestaAsync(request);

            Assert.Null(resultado);
        }
        [Fact]
        public async Task GenerarPregunta_FactoryDevuelveNull_LanzaException()
        {
            // Arrange
            _factoryMock
                .Setup(f => f.CrearEstrategia(TipoMiniJuego.Logica))
                .Returns((IMinijuegoStrategy?)null);

            // Act + Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await _service.GenerarPreguntaAsync(TipoMiniJuego.Logica);
            });

            _factoryMock.Verify(f => f.CrearEstrategia(TipoMiniJuego.Logica), Times.Once);
        }

        [Fact]
        public async Task ValidarRespuesta_RequestNull_LanzaNullReference()
        {
            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await _service.ValidarRespuestaAsync(null);
            });
        }


        [Fact]
        public async Task DebeValidar_Incorrecta()
        {
            Pregunta pregunta = new Pregunta { Id = 2, RespuestaCorrecta = "10", Tipo = TipoMiniJuego.Matematicas };
            _repoMock.Setup(r => r.ObtenerPorIdAsync(2)).ReturnsAsync(pregunta);

            ValidarRequest request = new ValidarRequest { PreguntaId = 2, Respuesta = "5" };

            ValidacionResponse? resultado = await _service.ValidarRespuestaAsync(request);

            Assert.NotNull(resultado);
            Assert.False(resultado!.EsCorrecta);
            Assert.Equal("Respuesta incorrecta.", resultado.Mensaje);
        }
    }
}
