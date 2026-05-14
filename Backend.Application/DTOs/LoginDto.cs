namespace Backend.Application.DTOs;

/// <summary>
/// Objeto de Transferencia de Datos (DTO) para la autenticación de usuarios.
/// Contiene las credenciales necesarias para validar la identidad de un usuario en el sistema.
/// </summary>
public class LoginDto
{
    /// <summary>
    /// Correo electrónico del usuario que intenta acceder.
    /// Actúa como el nombre de usuario único en el sistema.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Contraseña en texto plano proporcionada durante el intento de inicio de sesión.
    /// Será verificada contra el hash almacenado en la base de datos.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}