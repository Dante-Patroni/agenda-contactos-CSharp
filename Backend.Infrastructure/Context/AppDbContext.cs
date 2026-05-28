using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Context;

/// <summary>
/// Contexto de la base de datos para la aplicación.
/// Actúa como la unidad de trabajo y proporciona el acceso a las tablas mediante Entity Framework Core.
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Inicializa una nueva instancia de <see cref="AppDbContext"/> con las opciones de configuración proporcionadas.
    /// </summary>
    /// <param name="options">Opciones de configuración (ConnectionString, Motor de DB, etc.).</param>
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    /// <summary>
/// Conjunto de datos (tabla) para la entidad <see cref="Contacto"/>.
/// </summary>
/// <remarks>
/// Se utiliza 'Set&lt;Contacto&gt;()' para asegurar que la propiedad no sea nula 
/// y cumpla con las reglas de nulabilidad de C#.
/// </remarks>
public DbSet<Contacto> Contactos => Set<Contacto>();

/// <summary>
/// Conjunto de datos (tabla) para la entidad <see cref="User"/>.
/// </summary>
public DbSet<User> Users => Set<User>();
}
