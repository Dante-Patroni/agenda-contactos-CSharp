using System.Reflection;
using Microsoft.OpenApi.Models;
using Backend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Interfaces;
using Backend.Infrastructure.Repositories;
using Backend.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// --- Registro de Servicios (Dependency Injection) ---

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

/* * Configuración de Swagger para generar la documentación de la API.
 * Se utiliza para incluir los archivos XML generados por cada capa.
 */
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Backend API de Contactos",
        Version = "v1",
        Description = "API REST para la gestión de contactos desarrollada con ASP.NET Core."
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

/* * Configuración del contexto de base de datos MySQL.
 */
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    );
});

// --- Inyección de dependencias ---

// Repositorios
builder.Services.AddScoped<IContactoRepository, ContactoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
// Servicios
builder.Services.AddScoped<ContactoService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

// --- Configuración del Pipeline de solicitudes HTTP ---

/* * Habilita Swagger solo en entorno de desarrollo.
 */
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

/* * Inicia la ejecución de la aplicación.
 */
app.Run();