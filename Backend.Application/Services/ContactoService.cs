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
    /// <returns>Una lista de objetos <see cref="ContactoResponseDto"/>.</returns>
    public async Task<List<ContactoResponseDto>> ObtenerTodosAsync()
    {
        var contactos = await _contactoRepository.ObtenerTodosAsync();

        // Proyecta cada entidad Contacto a un nuevo ContactoResponseDto
        return contactos.Select(contacto => new ContactoResponseDto
        {
            Id = contacto.Id,
            Nombre = contacto.Nombre,
            Apellido = contacto.Apellido,
            Direccion = contacto.Direccion,
            Telefono = contacto.Telefono,
            Email = contacto.Email,
            Genero = contacto.Genero
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
            Email = contacto.Email,
            Apellido = contacto.Apellido,
            Direccion = contacto.Direccion,
            Genero = contacto.Genero
        };
    }

    /// <summary>
    /// Procesa la creación de un nuevo contacto.
    /// </summary>
    /// <param name="dto">DTO con los datos para crear el contacto.</param>
    /// <param name="userId">
    /// Identificador del usuario propietario del contacto.
    /// </param>
    /// <returns>
    /// El nuevo contacto creado representado como ContactoResponseDto.
    /// </returns>
    public async Task<ContactoResponseDto> CrearAsync(
    CrearContactoDto dto,
    int userId
)
    {
        // Mapeo de DTO de entrada a Entidad de dominio
        var contacto = new Contacto
        {
            Nombre = dto.Nombre,
            Telefono = dto.Telefono,
            Email = dto.Email,
            Apellido = dto.Apellido,
            Direccion = dto.Direccion,
            Genero = dto.Genero,
            UserId = userId
        };

        var contactoCreado = await _contactoRepository.CrearAsync(contacto);

        // Mapeo de Entidad creada (ya con ID) a DTO de respuesta
        return new ContactoResponseDto
        {
            Id = contactoCreado.Id,
            Nombre = contactoCreado.Nombre,
            Telefono = contactoCreado.Telefono,
            Email = contactoCreado.Email,
            Apellido = contactoCreado.Apellido,
            Direccion = contactoCreado.Direccion,
            Genero = contactoCreado.Genero
        };
    }

    /// <summary>
    /// Gestiona la actualización de los datos de un contacto existente.
    /// </summary>
    /// <param name="id">Identificador único del contacto a modificar.</param>
    /// <param name="dto">DTO con los nuevos datos actualizados.</param>
    /// <returns>
    /// Un <see cref="ContactoResponseDto"/> con los datos actualizados si el contacto existía; 
    /// de lo contrario, null.
    /// </returns>
    public async Task<ContactoResponseDto?> EditarAsync(int id, CrearContactoDto dto)
    {
        // Transformación de los datos del DTO a la entidad de dominio
        var contacto = new Contacto
        {
            Id = id,
            Nombre = dto.Nombre,
            Telefono = dto.Telefono,
            Email = dto.Email,
            Apellido = dto.Apellido,
            Direccion = dto.Direccion,
            Genero = dto.Genero
        };

        var contactoEditado = await _contactoRepository.EditarAsync(id, contacto);

        if (contactoEditado is null)
        {
            return null;
        }

        // Retorna el resultado mapeado nuevamente a DTO de respuesta
        return new ContactoResponseDto
        {
            Id = contactoEditado.Id,
            Nombre = contactoEditado.Nombre,
            Telefono = contactoEditado.Telefono,
            Email = contactoEditado.Email,
            Apellido = contactoEditado.Apellido,
            Direccion = contactoEditado.Direccion,
            Genero = contactoEditado.Genero
        };
    }

    /// <summary>
    /// Procesa la eliminación de un contacto del sistema.
    /// </summary>
    /// <param name="id">El identificador único del contacto a eliminar.</param>
    /// <returns>
    /// true si el contacto se eliminó correctamente; 
    /// de lo contrario, false si el contacto no fue encontrado.
    /// </returns>
    public async Task<bool> EliminarAsync(int id)
    {
        // Delega la responsabilidad de la eliminación física a la capa de infraestructura (Repositorio)
        return await _contactoRepository.EliminarAsync(id);
    }
}