🎮 API de Minijuegos – Trabajo Domiciliario Obligatorio 2

🏫 Instituto CTC – Colonia
📘 Materia: Diseño y Desarrollo de Aplicaciones
🧱 Plataforma: ASP.NET Core .NET 8 Web API
📅 Fecha: 07/11/2025
✨ Patrones implementados: Strategy · Repository · Factory · DTOs · Inyección de Dependencias
🚀 Mejoras REST: Versionado de API · Rate Limiting · Swagger Documentado

🧩 Estructura general del proyecto
⚙️ Program.cs

🟩 Arranca el servidor

Registra servicios, repositorios y estrategias.

Publica la documentación Swagger de forma automática.

Configura Rate Limiting.

Define rutas hacia los controladores usando el versionado de API.

Ejemplo de ruta base del proyecto:

/api/v1/minijuegos

🧭 Controllers

🟦 Reciben y responden requests HTTP

Exponen endpoints RESTful organizados por recursos.

Seleccionan el código de estado HTTP apropiado:
200 OK, 400 BadRequest, 404 NotFound, etc.

Documentados con /// <summary> y visibles en Swagger UI.

🧠 Services

🟪 Lógica de negocio

Generación y validación de preguntas.

Usa Factory para seleccionar la estrategia correcta.

No accede a DB → delega al Repository.

🧱 Models

🟨 Definen la estructura de datos

Pregunta (Entidad persistida en DB)

DTOs:

PreguntaResponse

ValidarRequest

ValidacionResponse

Separan el formato interno del externo → seguridad + flexibilidad.

🏗️ Infrastructure

🟥 Acceso a datos (EF Core + Repository)

ApplicationDbContext administra la base de datos.

PreguntaRepositorio abstrae la persistencia.

🧠 Patrones de diseño implementados
🧩 Strategy – Lógica desacoplada por tipo de minijuego

Se crean 3 estrategias independientes:

Estrategia	Archivo
Matemática	MatematicasStrategy.cs
Memoria	MemoriaStrategy.cs
Lógica	LogicaStrategy.cs

✔ Open/Closed
✔ Single Responsibility

🏭 Factory – Selección automática de estrategia
_factory.ObtenerEstrategia(tipoMinijuego);


✔ El servicio no usa if / switch
✔ Fácil de extender con nuevos juegos

🗂 Repository – Acceso a BD aislado

El servicio no conoce EF Core, solo una interfaz:

IPreguntaRepositorio


✔ Bajo acoplamiento
✔ Fácil de testear
✔ Cumple DIP

🧱 DTOs – Seguridad en la API

Previenen exponer internamente la DB y permiten validaciones automáticas.

✔ Responses limpias y específicas
✔ No se filtran campos innecesarios

🚀 Mejoras REST para Nota Extra
🏷 Versionado de API

Implementado usando rutas:

[Route("api/v1/[controller]")]


✔ Compatibilidad futura garantizada
✔ Buenas prácticas profesionales

🔒 Rate Limiting (Anti-Spam)

Evita abuso de endpoints sensibles como validación repetitiva.

📌 Configuración aplicada:

Límite: 5 requests / 10 segundos por IP

Protección simple ante DoS y bots

Aumenta la seguridad y estabilidad del servicio

🔄 Flujo completo de uso

1️⃣ GET /api/v1/minijuegos/generar/{tipo}
→ Devuelve una pregunta y se guarda en BD.

2️⃣ POST /api/v1/minijuegos/validar
→ Devuelve si la respuesta fue correcta o no.

✔ Funciones separadas
✔ Reglas claras de negocio

📁 Estructura final del proyecto

MinijuegosAPI.sln
├── MinijuegosAPI/
│   ├── Controllers/
│   │   └── MinijuegosController.cs
│   ├── Models/
│   │   ├── Entidades/
│   │   │   └── Pregunta.cs
│   │   └── Dtos/
│   │       ├── PreguntaResponse.cs
│   │       ├── ValidarRequest.cs
│   │       └── ValidacionResponse.cs
│   ├── Services/
│   │   ├── IMinijuegoService.cs
│   │   ├── MinijuegoService.cs
│   │   ├── Estrategias/
│   │   │   ├── MatematicasStrategy.cs
│   │   │   ├── MemoriaStrategy.cs
│   │   │   └── LogicaStrategy.cs
│   │   └── Factories/
│   │       ├── IMinijuegoFactory.cs
│   │       └── MinijuegoFactory.cs
│   ├── Infrastructure/
│   │   ├── Data/
│   │   │   └── ApplicationDbContext.cs
│   │   └── Repositorio/
│   │       ├── IPreguntaRepositorio.cs
│   │       └── PreguntaRepositorio.cs
│   └── Program.cs
└── MinijuegosAPI.Tests/
    ├── Controllers/
    │   └── MinijuegoControllerTest.cs
    ├── Estrategias/
    │   ├── LogicaStrategyTest.cs
    │   ├── MatematicasStrategyTest.cs
    │   └── MemoriaStrategyTest.cs
    ├── Services/
    │   └── MinijuegoServiceTests.cs
    ├── coveragereport/
    ├── TestResults/
    ├── coverage.cobertura.xml
    └── MinijuegosAPI.Tests.csproj


📊 Resultado Final

✔ API con patrones profesionales
✔ Arquitectura limpia y mantenible
✔ Versionado + Rate Limiting + Swagger
✔ Aplicación real lista para producción

👤 Autor

🧑‍💻 Ignacio Negro
📆 Última actualización: 20/11/2025
💯 Trabajo cumplido + mejoras para nota extra 🚀
