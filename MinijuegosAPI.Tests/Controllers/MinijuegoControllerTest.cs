using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using MinijuegosAPI.Controllers;
using MinijuegosAPI.Models.Dtos;
using MinijuegosAPI.Models.Entidades;
using MinijuegosAPI.Services;

namespace MinijuegosAPI.Tests.Controllers
{
    
    public class MinijuegosControllerTests
    {
        private readonly Mock<IMinijuegoService> _mockMinijuegoService;
        
        private readonly MinijuegosController _controller;

        
        public MinijuegosControllerTests()
        {

            _mockMinijuegoService = new Mock<IMinijuegoService>();
            _controller = new MinijuegosController(_mockMinijuegoService.Object);
        }

      
        [Fact]
        
        public async Task ObtenerPregunta_RetornaOkYPregunta()
        {
            
            PreguntaResponse preguntaStub = new PreguntaResponse 
            {
                PreguntaId = 10,
                Tipo = "matematicas",
                Datos = new { a = 5, b = 7 },
                FechaCreacion = DateTime.UtcNow
            };

            
            _mockMinijuegoService
                .Setup(service => service.GenerarPreguntaAsync(TipoMiniJuego.Matematicas))
                .ReturnsAsync(preguntaStub);

            IActionResult resultado = await _controller.ObtenerPregunta(TipoMiniJuego.Matematicas); 
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(resultado); 
            PreguntaResponse preguntaRetornada = Assert.IsType<PreguntaResponse>(okResult.Value); 

            Assert.Equal(10, preguntaRetornada.PreguntaId);
            Assert.Equal("matematicas", preguntaRetornada.Tipo);

            // 4. Verificar Interacción (Mocking): Asegurar que el método del servicio fue llamado EXACTAMENTE una vez.
            _mockMinijuegoService.Verify(service =>
                service.GenerarPreguntaAsync(TipoMiniJuego.Matematicas), Times.Once,
                "Se verifica que el Controller llamó al servicio de generación de pregunta."
            );
        }

        

        [Fact]
        public async Task ObtenerPregunta_ServicioRetornaNull_RetornaNotFound()
        {
          
            _mockMinijuegoService
                .Setup(service => service.GenerarPreguntaAsync(TipoMiniJuego.Matematicas))
                .ReturnsAsync((PreguntaResponse?)null);

            
            IActionResult resultado = await _controller.ObtenerPregunta(TipoMiniJuego.Matematicas); 
            NotFoundObjectResult notFound = Assert.IsType<NotFoundObjectResult>(resultado); 
            Assert.Equal("No se pudo generar la pregunta", notFound.Value);
            _mockMinijuegoService.Verify(service =>
                service.GenerarPreguntaAsync(TipoMiniJuego.Matematicas), Times.Once,
                "Se verifica que el servicio fue llamado para intentar generar la pregunta."
            );
        }

        
        [Fact]
        public async Task ValidarRespuesta_RespuestaCorrecta_RetornaOkYResultadoPositivo()
        {
            
            ValidarRequest request = new ValidarRequest // Usando tipado explícito
            {
                PreguntaId = 1,
                Respuesta = "123"
            };

            
            ValidacionResponse respuestaEsperada = new ValidacionResponse 
            {
                EsCorrecta = true, 
                Mensaje = "¡Respuesta correcta!",
                RespuestaCorrecta = "123",
                TipoMinijuego = "matematicas"
            };

            
            _mockMinijuegoService
                .Setup(service => service.ValidarRespuestaAsync(It.Is<ValidarRequest>(r => r.PreguntaId == 1)))
                .ReturnsAsync(respuestaEsperada);

            
            IActionResult resultado = await _controller.ValidarRespuesta(request); 

            
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(resultado); 
            ValidacionResponse contenido = Assert.IsType<ValidacionResponse>(okResult.Value); 

            
            Assert.True(contenido.EsCorrecta, "Se afirma que la propiedad EsCorrecta es verdadera.");

            
            Assert.Equal("¡Respuesta correcta!", contenido.Mensaje);

            
            _mockMinijuegoService.Verify(service =>
                service.ValidarRespuestaAsync(request), Times.Once,
                "Se verifica que el Controller delegó la validación al servicio, evitando la lógica real de la BD."
            );
        }

     

        [Fact]
        public async Task ValidarRespuesta_PreguntaNoEncontrada_RetornaNotFound()
        {
            
            ValidarRequest request = new ValidarRequest 
            {
                PreguntaId = 999, 
                Respuesta = "prueba"
            };

            
            _mockMinijuegoService
                .Setup(service => service.ValidarRespuestaAsync(It.Is<ValidarRequest>(r => r.PreguntaId == 999)))
                .ReturnsAsync((ValidacionResponse?)null);

            
            IActionResult resultado = await _controller.ValidarRespuesta(request); 
            NotFoundObjectResult notFound = Assert.IsType<NotFoundObjectResult>(resultado); 

            
            Assert.Equal("Pregunta no encontrada", notFound.Value?.ToString());

            
            _mockMinijuegoService.Verify(service =>
                service.ValidarRespuestaAsync(request), Times.Once,
                "Se verifica que el servicio fue llamado."
            );
        }
    }
}