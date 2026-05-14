using Backend.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Backend.Application.Services;
namespace Backend.API.Controllers;

/// <summary>
/// Controlador encargado de gestionar las operaciones de autenticación y registro de usuarios.
/// Expone los endpoints necesarios para la seguridad del sistema bajo la ruta base 'api/auth'.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="AuthController"/> inyectando el servicio de autenticación.
    /// </summary>
    /// <param name="authService">Servicio que contiene la lógica de negocio para la gestión de usuarios.</param>
    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Procesa la solicitud de registro para un nuevo usuario en el sistema.
    /// </summary>
    /// <param name="dto">Objeto de transferencia de datos con la información del registro.</param>
    /// <returns>
    /// Un <see cref="IActionResult"/> que representa el resultado de la operación:
    /// - 200 OK: Si el registro fue exitoso.
    /// - 400 BadRequest: Si el correo electrónico ya se encuentra en uso o hay errores de validación.
    /// </returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var resultado = await _authService.RegisterAsync(dto);

        if (resultado == "El email ya está registrado")
        {
            return BadRequest(resultado);
        }

        return Ok(resultado);
    }

   /// <summary>
    /// Procesa la solicitud de inicio de sesión y devuelve un token de acceso.
    /// Valida las credenciales a través del servicio de autenticación y encapsula la respuesta.
    /// </summary>
    /// <param name="dto">Objeto con las credenciales de inicio de sesión (Email y Password).</param>
    /// <returns>
    /// Un <see cref="IActionResult"/> que contiene:
    /// - 200 OK: Un objeto anónimo con el token generado si la autenticación es exitosa.
    /// - 401 Unauthorized: Un mensaje de error si las credenciales son inválidas.
    /// </returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        // Ejecuta la lógica de validación y generación de token en la capa de aplicación
        var resultado = await _authService.LoginAsync(dto);

        // Control de flujo basado en la respuesta del servicio
        if (resultado == "Usuario o contraseña incorrectos")
        {
            return Unauthorized(resultado);
        }

        // Si la autenticación es correcta, el resultado contiene el token JWT
        // Se devuelve envuelto en un objeto JSON para mayor compatibilidad con el cliente
        return Ok(new
        {
            token = resultado
        });
    }
}