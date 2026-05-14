namespace Backend.Domain.Entities;

/// <summary>
/// Representa la entidad de usuario dentro del sistema.
/// Almacena la información de identidad, credenciales protegidas y privilegios de acceso.
/// </summary>
public class User
{
    /// <summary>
    /// Identificador único del usuario (Clave primaria).
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre completo o alias del usuario.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Dirección de correo electrónico, utilizada como identificador único para el inicio de sesión.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Contraseña almacenada en formato hash para garantizar la seguridad.
    /// Nunca debe almacenarse la contraseña en texto plano en esta propiedad.
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Rol asignado al usuario para el control de acceso (RBAC). 
    /// Por defecto se asigna el valor "User".
    /// </summary>
    public string Rol { get; set; } = "User";

    /// <summary>
    /// Navigation Property - User 1:N Contactos
    /// </summary>
    public ICollection<Contacto> Contactos { get; set; }
        = new List<Contacto>();
}