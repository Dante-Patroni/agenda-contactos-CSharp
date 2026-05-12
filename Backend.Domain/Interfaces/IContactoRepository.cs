using Backend.Domain.Entities;

namespace Backend.Domain.Interfaces;

public interface IContactoRepository
{
    Task<List<Contacto>> ObtenerTodosAsync();

    Task<Contacto?> ObtenerPorIdAsync(int id);

    Task<Contacto> CrearAsync(Contacto contacto);

    Task<Contacto?> EditarAsync(int id, Contacto contacto);
}