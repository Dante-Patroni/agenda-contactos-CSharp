namespace Backend.Application.DTOs;

/// <summary>
/// Objeto de Transferencia de Datos (DTO) para la respuesta de contactos.
/// Se utiliza para enviar información desde la API hacia el cliente de forma segura.
/// Incluye el identificador único generado por el sistema.
/// </summary>
public class ContactoResponseDto
{
    /// <summary>
    /// Identificador único del contacto en la base de datos.
    /// </summary>
    /// <example>1</example>
    public int Id { get; set; }

    /// <summary>
    /// Nombre completo o alias del contacto.
    /// </summary>
    /// <example>Juan Pérez</example>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Número de teléfono registrado para el contacto.
    /// </summary>
    /// <example>+34 600 000 000</example>
    public string Telefono { get; set; } = string.Empty;

    /// <summary>
    /// Dirección de correo electrónico del contacto.
    /// </summary>
    /// <example>juan.perez@ejemplo.com</example>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Apellido de la persona.
    /// </summary>
    public string Apellido { get; set; } = string.Empty;

    /// <summary>
    /// Dirección física o de residencia.
    /// </summary>
    public string Direccion { get; set; } = string.Empty;

    /// <summary>
    /// Género de la persona.
    /// </summary>
    public string Genero { get; set; } = string.Empty;
}