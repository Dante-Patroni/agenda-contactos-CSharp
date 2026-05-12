using Backend.Domain.Entities;
using Backend.Domain.Interfaces;
using Backend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Repositories;

/// <summary>
/// Repositorio de persistencia para la entidad Contacto.
/// Implementa la interfaz IContactoRepository utilizando Entity Framework Core 
/// para interactuar con la base de datos.
/// </summary>
public class ContactoRepository : IContactoRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor para inyectar el contexto de la base de datos.
    /// </summary>
    /// <param name="context">Contexto de datos principal de la aplicación.</param>
    public ContactoRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene de forma asíncrona la lista completa de contactos desde la base de datos.
    /// </summary>
    /// <returns>Una tarea que representa la operación, con una lista de todos los objetos Contacto.</returns>
    public async Task<List<Contacto>> ObtenerTodosAsync()
    {
        // Traer todos los contactos de la base de datos
        return await _context.Contactos.ToListAsync();
    }

    /// <summary>
    /// Busca un contacto específico por su identificador único.
    /// </summary>
    /// <param name="id">Identificador único del contacto.</param>
    /// <returns>
    /// El objeto Contacto si se encuentra; 
    /// de lo contrario, null.
    /// </returns>
    public async Task<Contacto?> ObtenerPorIdAsync(int id)
    {
        // Traer un contacto por su id de la base de datos
        return await _context.Contactos.FindAsync(id);
    }

    /// <summary>
    /// Registra un nuevo contacto en la base de datos.
    /// </summary>
    /// <param name="contacto">Objeto con los datos del contacto a persistir.</param>
    /// <returns>El contacto creado con su ID generado por la base de datos.</returns>
    public async Task<Contacto> CrearAsync(Contacto contacto)
    {
        // Agregar un nuevo contacto a la base de datos
        _context.Contactos.Add(contacto);
        await _context.SaveChangesAsync();

        return contacto;
    }

    /// <summary>
    /// Actualiza los datos de un contacto existente en el sistema.
    /// </summary>
    /// <param name="id">Identificador del contacto que se desea modificar.</param>
    /// <param name="contacto">Objeto que contiene los nuevos valores (Nombre, Teléfono, Email).</param>
    /// <returns>
    /// El objeto Contacto actualizado si la operación tuvo éxito;
    /// null si el contacto original no fue encontrado.
    /// </returns>
    public async Task<Contacto?> EditarAsync(int id, Contacto contacto)
    {
        var contactoExistente = await _context.Contactos.FindAsync(id);

        // Si el contacto no existe, retorna null para manejar el error en niveles superiores
        if (contactoExistente is null)
        {
            return null;
        }

        // Mapeo manual de campos actualizables
        contactoExistente.Nombre = contacto.Nombre;
        contactoExistente.Telefono = contacto.Telefono;
        contactoExistente.Email = contacto.Email;

        await _context.SaveChangesAsync();

        return contactoExistente;
    }
    /// <summary>
    /// Solicita la eliminación de un contacto al repositorio.
    /// </summary>
    /// <param name="id">Identificador único del contacto que se desea eliminar.</param>
    /// <returns>
    /// Una tarea que contiene true si el contacto fue eliminado exitosamente; 
    /// de lo contrario, false si el contacto no existía.
    /// </returns>
    public async Task<bool> EliminarAsync(int id)
    {
        var contacto =
            // Llama al método correspondiente en la capa de persistencia (Repositorio)
            await _context.Contactos.FindAsync(id);

        if (contacto is null)
        {
            return false;
        }

        _context.Contactos.Remove(contacto);

        await _context.SaveChangesAsync();

        return true;
    }
}