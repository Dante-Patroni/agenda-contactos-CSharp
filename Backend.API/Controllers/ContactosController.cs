using Backend.Application.DTOs;
using Backend.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

/// <summary>
/// Controlador para la gestión de contactos.
/// Proporciona endpoints para consultar y crear contactos mediante la lógica definida en ContactoService.
/// La ruta base es: /api/contactos
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ContactosController : ControllerBase
{
    private readonly ContactoService _contactoService;

    /// <summary>
    /// Constructor del controlador que inyecta el servicio de lógica de negocio.
    /// </summary>
    /// <param name="contactoService">Instancia de ContactoService para procesar los datos.</param>
    public ContactosController(ContactoService contactoService)
    {
        _contactoService = contactoService;
    }

    /// <summary>
    /// Recupera la lista de todos los contactos registrados.
    /// Método HTTP: GET /api/contactos
    /// </summary>
    /// <returns>
    /// Una lista de objetos <see cref="ContactoResponseDto"/> con estado 200 OK.
    /// </returns>
    [HttpGet]
    public async Task<ActionResult<List<ContactoResponseDto>>> ObtenerTodos()
    {
        var contactos = await _contactoService.ObtenerTodosAsync();

        return Ok(contactos);
    }

    /// <summary>
    /// Recupera un contacto específico mediante su identificador único.
    /// Método HTTP: GET /api/contactos/{id}
    /// </summary>
    /// <param name="id">ID numérico del contacto a buscar.</param>
    /// <returns>
    /// 200 OK si el contacto existe.
    /// 404 Not Found si el identificador no corresponde a ningún registro.
    /// </returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ContactoResponseDto>> ObtenerPorId(int id)
    {
        var contacto = await _contactoService.ObtenerPorIdAsync(id);

        if (contacto is null)
        {
            return NotFound();
        }

        return Ok(contacto);
    }

    /// <summary>
    /// Crea un nuevo registro de contacto en el sistema.
    /// Método HTTP: POST /api/contactos
    /// </summary>
    /// <param name="dto">Objeto con los datos del nuevo contacto (Nombre, Teléfono, Email).</param>
    /// <returns>
    /// 201 Created con el objeto creado.
    /// Incluye el header 'Location' con la URL para consultar el nuevo recurso.
    /// </returns>
    [HttpPost]
    public async Task<ActionResult<ContactoResponseDto>> Crear(CrearContactoDto dto)
    {
        var contactoCreado = await _contactoService.CrearAsync(dto);

        // Genera una respuesta 201 y apunta al método ObtenerPorId para localizar el recurso
        return CreatedAtAction(
            nameof(ObtenerPorId),
            new { id = contactoCreado.Id },
            contactoCreado
        );
    }
}