using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Backend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Interfaces;
using Backend.Infrastructure.Repositories;
using Backend.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
// --- Configuración de URL: Asegura  que escuche en todas las interfaces ---
builder.WebHost.UseUrls("http://0.0.0.0:5148"); 

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

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresar token JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
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

// Configuración avanzada de los servicios de autenticación en el contenedor de dependencias.
// Registra y parametriza el esquema JWT Bearer con validación estricta de entorno de producción.
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Define las reglas y parámetros de seguridad para la validación estricta de los tokens entrantes
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Obliga a validar que el emisor del token (Issuer) coincida con el esperado
            ValidateIssuer = true,

            // Obliga a validar que el receptor o destinatario del token (Audience) coincida con el esperado
            ValidateAudience = true,

            // Comprueba rigurosamente que la fecha actual esté dentro del rango de vigencia del token (que no haya expirado)
            ValidateLifetime = true,

            // Exige que el token esté firmado digitalmente y que dicha firma sea verificable
            ValidateIssuerSigningKey = true,

            // Establece el emisor válido recuperándolo del archivo de configuración (appsettings.json)
            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            // Establece la audiencia válida recuperándola del archivo de configuración (appsettings.json)
            ValidAudience = builder.Configuration["Jwt:Audience"],

            // Especifica la clave criptográfica simétrica secreta utilizada para validar la firma e integridad del token
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["Jwt:Key"]!
                )
            )
        };
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

/* * Inicia la ejecución de la aplicación.
 */
app.Run();