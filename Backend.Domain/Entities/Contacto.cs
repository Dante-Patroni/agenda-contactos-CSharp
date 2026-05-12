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
}