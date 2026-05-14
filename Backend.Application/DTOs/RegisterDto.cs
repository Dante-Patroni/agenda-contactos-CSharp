namespace Backend.Application.DTOs;

/// <summary>
/// Objeto de Transferencia de Datos (DTO) para el registro de nuevos usuarios.
/// Define la estructura de información requerida que debe enviar el cliente.
/// </summary>
public class RegisterDto
{
    /// <summary>
    /// Nombre completo o alias del usuario que desea registrarse.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Dirección de correo electrónico, utilizada como identificador único de acceso.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Contraseña en texto plano enviada por el cliente. 
    /// Esta será cifrada antes de persistirse en la base de datos.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}
