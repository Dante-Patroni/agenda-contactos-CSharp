using Backend.Domain.Entities;
namespace Backend.Domain.Interfaces;

/// <summary>
/// Define el contrato para el repositorio de gestión de usuarios.
/// Establece las operaciones de acceso a datos necesarias para la autenticación y registro.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Busca un usuario en el almacén de datos utilizando su dirección de correo electrónico.
    /// </summary>
    /// <param name="email">Email del usuario a buscar.</param>
    /// <returns>
    /// Una tarea que representa la operación asíncrona. 
    /// El resultado de la tarea contiene el <see cref="User"/> encontrado o null si no existe.
    /// </returns>
    Task<User?> ObtenerPorEmailAsync(string email);

    /// <summary>
    /// Persiste un nuevo usuario en el sistema.
    /// </summary>
    /// <param name="user">La entidad de usuario con los datos a registrar.</param>
    /// <returns>
    /// Una tarea que representa la operación asíncrona. 
    /// El resultado contiene la entidad <see cref="User"/> persistida, incluyendo su ID generado.
    /// </returns>
    Task<User> CrearAsync(User user);
}