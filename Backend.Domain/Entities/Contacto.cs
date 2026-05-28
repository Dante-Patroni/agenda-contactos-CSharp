namespace Backend.Domain.Entities;

/// <summary>
/// Representa la entidad de dominio para un Contacto.
/// Esta clase está mapeada directamente a la tabla de la base de datos.
/// </summary>
public class Contacto
{
    /// <summary>
    /// Identificador único del contacto (Clave primaria).
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre completo del contacto.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Número de teléfono o celular del contacto.
    /// </summary>
    public string Telefono { get; set; } = string.Empty;

    /// <summary>
    /// Dirección de correo electrónico del contacto.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Obtiene o establece el apellido del usuario o contacto.
    /// Se inicializa como una cadena vacía para evitar excepciones de referencia nula (NullReferenceException).
    /// </summary>
    public string Apellido { get; set; } = string.Empty;

    /// <summary>
    /// Obtiene o establece la dirección residencial o de correspondencia.
    /// Se inicializa como una cadena vacía para prevenir valores nulos durante el mapeo de datos.
    /// </summary>
    public string Direccion { get; set; } = string.Empty;

    /// <summary>
    /// Obtiene o establece el género o identidad de género de la persona.
    /// Se inicializa como una cadena vacía por defecto para garantizar la consistencia en la base de datos.
    /// </summary>
    public string Genero { get; set; } = string.Empty;

    // Foreign Key
    public int UserId { get; set; }

    /// <summary>
    /// Navigation Property
    /// </summary>
    public User User { get; set; } = null!;
}