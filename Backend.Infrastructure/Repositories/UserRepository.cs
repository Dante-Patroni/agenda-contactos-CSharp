using Backend.Domain.Entities;
using Backend.Domain.Interfaces;
using Backend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
namespace Backend.Infrastructure.Repositories;

/// <summary>
/// Implementación del repositorio de usuarios utilizando Entity Framework Core.
/// Proporciona los métodos para la persistencia y consulta de usuarios en la base de datos.
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="UserRepository"/> inyectando el contexto de datos.
    /// </summary>
    /// <param name="context">Contexto de base de datos de la aplicación.</param>
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Busca un usuario en la tabla de usuarios basado en su dirección de correo electrónico.
    /// </summary>
    /// <param name="email">Email del usuario a localizar.</param>
    /// <returns>
    /// El usuario encontrado que coincide con el email; de lo contrario, null.
    /// </returns>
    public async Task<User?> ObtenerPorEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    /// <summary>
    /// Registra un nuevo objeto de usuario en la base de datos de forma asíncrona.
    /// </summary>
    /// <param name="user">Entidad de usuario con los datos a persistir.</param>
    /// <returns>La entidad de usuario procesada y guardada.</returns>
    public async Task<User> CrearAsync(User user)
    {
        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return user;
    }
}