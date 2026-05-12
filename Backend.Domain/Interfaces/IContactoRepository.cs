using Backend.Domain.Entities;

namespace Backend.Domain.Interfaces;

/// <summary>
/// Interfaz que define el contrato para el repositorio de contactos.
/// Proporciona las abstracciones necesarias para las operaciones de persistencia (CRUD).
/// </summary>
public interface IContactoRepository
{
    /// <summary>
    /// Recupera de forma asíncrona todos los contactos almacenados.
    /// </summary>
    /// <returns>Una tarea con la lista de entidades <see cref="Contacto"/>.</returns>
    Task<List<Contacto>> ObtenerTodosAsync();

    /// <summary>
    /// Busca un contacto específico por su identificador único.
    /// </summary>
    /// <param name="id">Identificador numérico del contacto.</param>
    /// <returns>
    /// El objeto <see cref="Contacto"/> si existe; 
    /// de lo contrario, null.
    /// </returns>
    Task<Contacto?> ObtenerPorIdAsync(int id);

    /// <summary>
    /// Persiste un nuevo contacto en el almacén de datos.
    /// </summary>
    /// <param name="contacto">Entidad con la información a registrar.</param>
    /// <returns>El contacto creado, incluyendo su ID asignado.</returns>
    Task<Contacto> CrearAsync(Contacto contacto);

    /// <summary>
    /// Actualiza los datos de un contacto existente.
    /// </summary>
    /// <param name="id">ID del contacto a modificar.</param>
    /// <param name="contacto">Objeto con los nuevos valores para el contacto.</param>
    /// <returns>
    /// La entidad actualizada si la operación fue exitosa; 
    /// null si el contacto no fue encontrado.
    /// </returns>
    Task<Contacto?> EditarAsync(int id, Contacto contacto);

    /// <summary>
    /// Elimina de forma definitiva un contacto del sistema.
    /// </summary>
    /// <param name="id">Identificador único del contacto a remover.</param>
    /// <returns>
    /// true si el contacto fue eliminado con éxito; 
    /// false si no se encontró el contacto.
    /// </returns>
    Task<bool> EliminarAsync(int id);
}