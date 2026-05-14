namespace Backend.Application.DTOs;

/// <summary>
/// Objeto de Transferencia de Datos (DTO) que representa la respuesta de autenticación exitosa.
/// Contiene la credencial de acceso y los datos básicos del perfil del usuario para el cliente.
/// </summary>
public class AuthResponseDto
{
    /// <summary>
    /// Token de seguridad (generalmente JWT) utilizado para autorizar peticiones posteriores.
    /// Debe ser incluido en el encabezado HTTP 'Authorization' bajo el esquema 'Bearer'.
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Dirección de correo electrónico del usuario autenticado.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Rol asignado al usuario en el sistema. 
    /// Determina el nivel de acceso y permisos dentro de la aplicación.
    /// </summary>
    public string Rol { get; set; } = string.Empty;
}