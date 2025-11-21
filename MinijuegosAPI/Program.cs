using AspNetCoreRateLimit;
using Microsoft.EntityFrameworkCore;
using MinijuegosAPI.Infraestructura.Data;
using MinijuegosAPI.Infraestructura.Repositorio;
using MinijuegosAPI.Services;
using MinijuegosAPI.Services.Estrategias;
using MinijuegosAPI.Services.Factories;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[ExcludeFromCodeCoverage]
public partial class Program 
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddMemoryCache();
        builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        builder.Services.AddInMemoryRateLimiting();

        builder.Services.AddScoped<IPreguntaRepositorio, PreguntaRepositorio>();
        builder.Services.AddScoped<IMinijuegoFactory, MinijuegoFactory>();
        builder.Services.AddScoped<IMinijuegoService, MinijuegoService>();

        // Estrategias
        builder.Services.AddScoped<IMinijuegoStrategy, MatematicasStrategy>();
        builder.Services.AddScoped<IMinijuegoStrategy, MemoriaStrategy>();
        builder.Services.AddScoped<IMinijuegoStrategy, LogicaStrategy>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseIpRateLimiting();

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}