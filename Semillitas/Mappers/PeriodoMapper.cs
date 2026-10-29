using Semillitas.Api.Entities;
using Semillitas.Core.DTOs;

namespace Semillitas.Api.Mappers;

public static class PeriodoMapper
{
    public static PeriodoDTO ToDTO(this Periodo periodo) => new()
    {
        Id = periodo.Id,
        Nombre = periodo.Nombre,
        FechaInicio = periodo.FechaInicio,
        FechaFin = periodo.FechaFin
    };

    public static Periodo ToEntity(this CreatePeriodoDTO dto) => new()
    {
        Nombre = dto.Nombre.Trim(),
        FechaInicio = dto.FechaInicio,
        FechaFin = dto.FechaFin
    };

    public static void ApplyTo(this EditPeriodoDTO dto, Periodo periodo)
    {
        periodo.Nombre = dto.Nombre.Trim();
        periodo.FechaInicio = dto.FechaInicio;
        periodo.FechaFin = dto.FechaFin;
    }
}
