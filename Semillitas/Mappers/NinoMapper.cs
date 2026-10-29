using Semillitas.Api.Entities;
using Semillitas.Core.DTOs;

namespace Semillitas.Api.Mappers;

public static class NinoMapper
{
    public static NinoDTO ToDTO(this Nino nino) => new()
    {
        Id = nino.Id,
        Nombre = nino.Nombre,
        FechaNacimiento = nino.FechaNacimiento,
        TutorId = nino.TutorId,
        TutorNombre = nino.Tutor?.Nombre ?? string.Empty
    };

    public static Nino ToEntity(this CreateNinoDTO dto) => new()
    {
        Nombre = dto.Nombre.Trim(),
        FechaNacimiento = dto.FechaNacimiento,
        TutorId = dto.TutorId
    };
}
