using System.Threading.Tasks;
using Xunit;
using Moq;
using MinijuegosAPI.Services.Estrategias;
using MinijuegosAPI.Infraestructura.Repositorio;
using MinijuegosAPI.Models.Dtos;
using MinijuegosAPI.Models.Entidades;

namespace MinijuegosAPI.Tests.Estrategias
{
   
    public class MatematicasStrategyTests
    {

        [Fact]
       
        public async Task GenerarAsync_AlGuardar_DebeRetornarPreguntaDeTipoMatematicas()
        {
          //creo el mok aislando la capa de datos
            Mock<IPreguntaRepositorio> mockRepo = new Mock<IPreguntaRepositorio>();

            //simulo el compomrtamiento de la bd
            mockRepo.Setup(r => r.GuardarAsync(It.IsAny<Pregunta>()))
                .Callback<Pregunta>(pregunta => pregunta.Id = 123).Returns(Task.CompletedTask);

            //inyecto el mok con la dependencia "simulo"
            MatematicasStrategy estrategia = new MatematicasStrategy(mockRepo.Object);
                       
            PreguntaResponse resultado = await estrategia.GenerarAsync();
            Assert.NotNull(resultado);
            Assert.Equal(123, resultado.PreguntaId);
           Assert.Equal("matematicas", resultado.Tipo);
            // Me aseguro que el metodo  de guardar del repositorio fue llamado
            mockRepo.Verify(r =>
                r.GuardarAsync(It.IsAny<Pregunta>()),
                Times.Once
            );
        }
    }
}