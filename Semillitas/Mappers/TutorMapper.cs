using Semillitas.Api.Entities;
using Semillitas.Core.DTOs;

namespace Semillitas.Api.Mappers;

public static class TutorMapper
{
    public static TutorDTO ToDTO(this Tutor tutor) => new()
    {
        Id = tutor.Id,
        Nombre = tutor.Nombre,
        Telefono = tutor.Telefono,
        Email = tutor.Email
    };

    public static Tutor ToEntity(this CreateTutorDTO dto) => new()
    {
        Nombre = dto.Nombre.Trim(),
        Telefono = dto.Telefono.Trim(),
        Email = string.IsNullOrWhiteSpace(dto.Email) ? null : dto.Email.Trim()
    };
}
