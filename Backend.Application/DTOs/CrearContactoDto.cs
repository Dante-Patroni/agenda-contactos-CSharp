namespace Backend.Application.DTOs;

/// <summary>
/// Objeto de Transferencia de Datos (DTO) para la creación de un nuevo contacto.
/// Define la estructura de datos que el cliente debe enviar al sistema.
/// A diferencia de la entidad de dominio, este DTO no incluye un ID.
/// </summary>
public class CrearContactoDto
{
    /// <summary>
    /// Nombre completo o alias del contacto.
    /// </summary>
    /// <example>Juan Pérez</example>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Número de teléfono de contacto.
    /// </summary>
    /// <example>+34 600 000 000</example>
    public string Telefono { get; set; } = string.Empty;

    /// <summary>
    /// Dirección de correo electrónico del contacto.
    /// </summary>
    /// <example>juan.perez@ejemplo.com</example>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Obtiene o establece el apellido de la persona.
    /// Se inicializa como una cadena vacía por defecto para mitigar riesgos de <see cref="NullReferenceException"/>.
    /// </summary>
    public string Apellido { get; set; } = string.Empty;

    /// <summary>
    /// Obtiene o establece la dirección física o de residencia.
    /// Se inicializa como una cadena vacía para asegurar la consistencia durante la serialización y el mapeo de datos.
    /// </summary>
    public string Direccion { get; set; } = string.Empty;

    /// <summary>
    /// Obtiene o establece el género de la persona.
    /// Se inicializa como una cadena vacía para evitar valores nulos en el transporte de datos o persistencia.
    /// </summary>
    public string Genero { get; set; } = string.Empty;
}