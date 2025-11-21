using System.Threading.Tasks;
using Xunit;
using Moq;
using MinijuegosAPI.Services.Estrategias;
using MinijuegosAPI.Infraestructura.Repositorio;
using MinijuegosAPI.Models.Dtos;
using MinijuegosAPI.Models.Entidades;

namespace MinijuegosAPI.Tests.Estrategias
{
    public class MemoriaStrategyTests
    {
        [Fact]
        public async Task DebeGenerarPregunta_DeTipoMemoria()
        {
            Mock<IPreguntaRepositorio> repo = new Mock<IPreguntaRepositorio>();

            repo.Setup(r => r.GuardarAsync(It.IsAny<Pregunta>()))
                .Callback<Pregunta>(p => p.Id = 200)
                .Returns(Task.CompletedTask);

            MemoriaStrategy estrategia = new MemoriaStrategy(repo.Object);

            PreguntaResponse resultado = await estrategia.GenerarAsync();

            Assert.NotNull(resultado);
            Assert.Equal(200, resultado.PreguntaId);
            Assert.Equal("memoria", resultado.Tipo);
            repo.Verify(r => r.GuardarAsync(It.IsAny<Pregunta>()), Times.Once);
        }
    }
}
