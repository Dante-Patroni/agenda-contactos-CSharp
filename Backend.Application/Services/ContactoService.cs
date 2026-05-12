using Backend.Application.DTOs;
using Backend.Domain.Entities;
using Backend.Domain.Interfaces;

namespace Backend.Application.Services;

/// <summary>
/// Servicio de aplicación para la gestión de contactos.
/// Contiene la lógica de negocio y se encarga de la transformación entre 
/// entidades de dominio y objetos de transferencia de datos (DTOs).
/// </summary>
public class ContactoService
{
    private readonly IContactoRepository _contactoRepository;

    /// <summary>
    /// Constructor que inyecta la abstracción del repositorio de contactos.
    /// </summary>
    /// <param name="contactoRepository">Instancia del repositorio (IContactoRepository).</param>
    public ContactoService(IContactoRepository contactoRepository)
    {
        _contactoRepository = contactoRepository;
    }

    /// <summary>
    /// Obtiene todos los contactos y los transforma en una lista de DTOs de respuesta.
    /// </summary>
    /// <returns>Lista de objetos <see cref="ContactoResponseDto"/>.</returns>
    public async Task<List<ContactoResponseDto>> ObtenerTodosAsync()
    {
        var contactos = await _contactoRepository.ObtenerTodosAsync();

        // Proyecta cada entidad Contacto a un nuevo ContactoResponseDto
        return contactos.Select(contacto => new ContactoResponseDto
        {
            Id = contacto.Id,
            Nombre = contacto.Nombre,
            Telefono = contacto.Telefono,
            Email = contacto.Email
        }).ToList();
    }

    /// <summary>
    /// Busca un contacto por ID y lo devuelve mapeado a un DTO si existe.
    /// </summary>
    /// <param name="id">Identificador único del contacto.</param>
    /// <returns>
    /// Un <see cref="ContactoResponseDto"/> si el contacto se encuentra; 
    /// de lo contrario, null.
    /// </returns>
    public async Task<ContactoResponseDto?> ObtenerPorIdAsync(int id)
    {
        var contacto = await _contactoRepository.ObtenerPorIdAsync(id);

        if (contacto is null)
        {
            return null;
        }

        // Mapeo manual de Entidad a DTO de respuesta
        return new ContactoResponseDto
        {
            Id = contacto.Id,
            Nombre = contacto.Nombre,
            Telefono = contacto.Telefono,
            Email = contacto.Email
        };
    }

    /// <summary>
    /// Procesa la creación de un nuevo contacto.
    /// Transforma el DTO de entrada en una entidad, la persiste y devuelve el DTO de respuesta.
    /// </summary>
    /// <param name="dto">DTO con los datos para crear el contacto.</param>
    /// <returns>El nuevo contacto creado representado como <see cref="ContactoResponseDto"/>.</returns>
    public async Task<ContactoResponseDto> CrearAsync(CrearContactoDto dto)
    {
        // Mapeo de DTO de entrada a Entidad de dominio
        var contacto = new Contacto
        {
            Nombre = dto.Nombre,
            Telefono = dto.Telefono,
            Email = dto.Email
        };

        var contactoCreado = await _contactoRepository.CrearAsync(contacto);

        // Mapeo de Entidad creada (ya con ID) a DTO de respuesta
        return new ContactoResponseDto
        {
            Id = contactoCreado.Id,
            Nombre = contactoCreado.Nombre,
            Telefono = contactoCreado.Telefono,
            Email = contactoCreado.Email
        };
    }
}