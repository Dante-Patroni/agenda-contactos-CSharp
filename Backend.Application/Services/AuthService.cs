using Backend.Application.DTOs;
using Backend.Domain.Entities;
using Backend.Domain.Interfaces;

namespace Backend.Application.Services;

/// <summary>
/// Proporciona los servicios de lógica de negocio relacionados con la seguridad y gestión de usuarios.
/// Coordina las operaciones entre los controladores y la persistencia de datos.
/// </summary>
public class AuthService
{
    private readonly IUserRepository _userRepository;
private readonly JwtService _jwtService;
    /// <summary>
    /// Inicializa una nueva instancia de <see cref="AuthService"/> inyectando la abstracción del repositorio de usuarios.
    /// </summary>
    /// <param name="userRepository">Interfaz del repositorio para acceder a los datos de usuario.</param>
    /// <param name="jwtService">Servicio para la generación de tokens JWT.</param>
    public AuthService(IUserRepository userRepository, JwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    /// <summary>
    /// Orquestra el proceso de registro de un nuevo usuario, validando duplicados y protegiendo credenciales.
    /// </summary>
    /// <param name="dto">Datos del registro proporcionados por el cliente.</param>
    /// <returns>
    /// Una cadena de texto que indica el resultado de la operación (éxito o mensaje de error de validación).
    /// </returns>
    public async Task<string> RegisterAsync(RegisterDto dto)
    {
        // Validación de existencia previa para evitar duplicidad de cuentas
        var usuarioExistente = await _userRepository.ObtenerPorEmailAsync(dto.Email);

        if (usuarioExistente is not null)
        {
            return "El email ya está registrado";
        }

        // Cifrado de la contraseña utilizando el algoritmo BCrypt para almacenamiento seguro
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        // Mapeo de DTO a Entidad de Dominio
        var user = new User
        {
            Nombre = dto.Nombre,
            Email = dto.Email,
            PasswordHash = passwordHash
        };

        // Persistencia definitiva en la base de datos a través del repositorio
        await _userRepository.CrearAsync(user);

        return "Usuario registrado correctamente";
    }

    /// <summary>
    /// Valida las credenciales del usuario y genera un token de acceso si son correctas.
    /// </summary>
    /// <param name="dto">Datos de acceso proporcionados por el cliente.</param>
    /// <returns>
    /// Una cadena que contiene el token JWT si la autenticación es exitosa; 
    /// de lo contrario, un mensaje de error genérico.
    /// </returns>
    public async Task<string> LoginAsync(LoginDto dto)
    {
        // Intenta recuperar el usuario desde el repositorio por su email
        var usuario = await _userRepository.ObtenerPorEmailAsync(dto.Email);

        // Si el usuario no existe, retorna un mensaje de error genérico por seguridad
        if (usuario is null)
        {
            return "Usuario o contraseña incorrectos";
        }

        // Verifica si la contraseña proporcionada coincide con el hash almacenado
        bool passwordValida = BCrypt.Net.BCrypt.Verify(
            dto.Password,
            usuario.PasswordHash
        );

        // Si la contraseña no es válida, retorna el mismo mensaje genérico
        if (!passwordValida)
        {
            return "Usuario o contraseña incorrectos";
        }

        // Una vez autenticado, genera el token de seguridad para el usuario
        string token = _jwtService.GenerarToken(usuario);

        return token;
    }


}