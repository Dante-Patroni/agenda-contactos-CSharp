using Backend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Interfaces;
using Backend.Infrastructure.Repositories;
using Backend.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString =
        builder.Configuration.GetConnectionString("DefaultConnection");

    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    );
});
// Injección de dependencia para el repositorio de contatos
builder.Services.AddScoped<IContactoRepository, ContactoRepository>();
// Inyección de dependencia para el servicio de contactos
builder.Services.AddScoped<ContactoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();