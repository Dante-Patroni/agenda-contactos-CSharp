using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Application.Services;

/// <summary>
/// Servicio encargado de la generación y gestión de tokens de seguridad JWT (JSON Web Token).
/// Permite crear credenciales firmadas para que el cliente pueda realizar peticiones autenticadas.
/// </summary>
public class JwtService
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="JwtService"/> inyectando la configuración de la aplicación.
    /// </summary>
    /// <param name="configuration">Propiedades de configuración donde se encuentran las claves y parámetros del JWT.</param>
    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Genera un token JWT firmado para un usuario específico.
    /// </summary>
    /// <param name="user">Entidad del usuario para el cual se emitirá el token.</param>
    /// <returns>Una cadena de texto que contiene el token JWT generado.</returns>
    public string GenerarToken(User user)
    {
        // Definición de los "Claims" (Aseveraciones): Información de identidad embebida en el token.
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        // Configuración de la clave simétrica utilizando la clave secreta definida en la configuración.
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"]!
            )
        );

        // Definición de las credenciales de firma utilizando el algoritmo HMAC SHA256.
        var credentials = new SigningCredentials(
            key, 
            SecurityAlgorithms.HmacSha256
        );

        // Creación del objeto del token con sus parámetros de emisor, audiencia, expiración y firma.
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: credentials
        );

        // Serialización del objeto token a una cadena de texto (JWT estándar).
        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}