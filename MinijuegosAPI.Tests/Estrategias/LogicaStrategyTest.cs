using System.Threading.Tasks;
using Xunit;
using Moq;
using MinijuegosAPI.Services.Estrategias;
using MinijuegosAPI.Infraestructura.Repositorio;
using MinijuegosAPI.Models.Dtos;
using MinijuegosAPI.Models.Entidades;

namespace MinijuegosAPI.Tests.Estrategias
{
    public class LogicaStrategyTests
    {
        [Fact]
        public async Task DebeGenerarPregunta_DeTipoLogica()
        {
            Mock<IPreguntaRepositorio> repo = new Mock<IPreguntaRepositorio>();

            repo.Setup(r => r.GuardarAsync(It.IsAny<Pregunta>()))
                .Callback<Pregunta>(p => p.Id = 300)
                .Returns(Task.CompletedTask);

            LogicaStrategy estrategia = new LogicaStrategy(repo.Object);

            PreguntaResponse resultado = await estrategia.GenerarAsync();

            Assert.NotNull(resultado);
            Assert.Equal(300, resultado.PreguntaId);
            Assert.Equal("logica", resultado.Tipo); 
            repo.Verify(r => r.GuardarAsync(It.IsAny<Pregunta>()), Times.Once);
        }
    }
}
