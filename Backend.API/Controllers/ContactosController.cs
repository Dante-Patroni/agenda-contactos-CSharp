using Backend.Application.DTOs;
using Backend.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace Backend.API.Controllers;



/// <summary>
/// Controlador encargado de gestionar las operaciones de la agenda de contactos.
/// Requiere autenticación obligatoria para todos sus endpoints mediante el atributo [Authorize].
/// Expone las rutas bajo el patrón base 'api/contactos'.
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ContactosController : ControllerBase
{
    // Los endpoints de este controlador heredan la restricción de seguridad [Authorize]

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
    /// </summary>
    /// <returns>Una lista de objetos <see cref="ContactoResponseDto"/>.</returns>
    [HttpGet]
    public async Task<ActionResult<List<ContactoResponseDto>>> ObtenerTodos()
    {
        var contactos = await _contactoService.ObtenerTodosAsync();
        return Ok(contactos);
    }

    /// <summary>
    /// Recupera un contacto específico mediante su identificador único.
    /// </summary>
    /// <param name="id">ID numérico del contacto a buscar.</param>
    /// <returns>El contacto solicitado o un error 404 si no existe.</returns>
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
    /// Crea un nuevo registro de contacto.
    /// </summary>
    /// <param name="dto">Datos del contacto a crear.</param>
    /// <returns>El contacto recién creado y la ubicación del recurso.</returns>
    [HttpPost]
    public async Task<ActionResult<ContactoResponseDto>> Crear(
    CrearContactoDto dto
)
    {
        var userIdClaim = User.FindFirst(
            System.Security.Claims.ClaimTypes.NameIdentifier
        )?.Value;

        Console.WriteLine($"USER ID TOKEN: {userIdClaim}");

        int userId = int.Parse(userIdClaim!);

        var contactoCreado = await _contactoService.CrearAsync(dto, userId);

        return CreatedAtAction(
            nameof(ObtenerPorId),
            new { id = contactoCreado.Id },
            contactoCreado
        );
    }

    /// <summary>
    /// Actualiza un contacto existente identificado por su ID.
    /// </summary>
    /// <param name="id">ID del contacto a modificar.</param>
    /// <param name="dto">Nuevos datos para el contacto.</param>
    /// <returns>
    /// 200 OK con los datos actualizados si tiene éxito.
    /// 404 Not Found si el ID no corresponde a ningún contacto.
    /// </returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<ContactoResponseDto>> Editar(int id, CrearContactoDto dto)
    {
        var contactoEditado = await _contactoService.EditarAsync(id, dto);

        if (contactoEditado is null)
        {
            return NotFound();
        }

        return Ok(contactoEditado);
    }

    /// <summary>
    /// Elimina un contacto de forma permanente.
    /// </summary>
    /// <param name="id">ID del contacto a eliminar.</param>
    /// <returns>
    /// 204 No Content si se eliminó correctamente.
    /// 404 Not Found si el contacto no fue encontrado.
    /// </returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Eliminar(int id)
    {
        var eliminado = await _contactoService.EliminarAsync(id);

        if (!eliminado)
        {
            return NotFound();
        }

        return NoContent();
    }
}