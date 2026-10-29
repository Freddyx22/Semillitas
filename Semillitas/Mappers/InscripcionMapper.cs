using Semillitas.Api.Entities;
using Semillitas.Core.DTOs;

namespace Semillitas.Api.Mappers;

public static class InscripcionMapper
{
    public static InscripcionDTO ToDTO(this Inscripcion inscripcion) => new()
    {
        Id = inscripcion.Id,
        NinoId = inscripcion.NinoId,
        NinoNombre = inscripcion.Nino?.Nombre ?? string.Empty,
        PeriodoId = inscripcion.PeriodoId,
        PeriodoNombre = inscripcion.Periodo?.Nombre ?? string.Empty,
        FechaRegistro = inscripcion.FechaRegistro,
        Total = inscripcion.Total,
        Pagada = inscripcion.Pagada,
        FechaPago = inscripcion.FechaPago,
        FechaVencimiento = inscripcion.FechaVencimiento,
        Vencida = !inscripcion.Pagada && inscripcion.FechaVencimiento < DateOnly.FromDateTime(DateTime.Today),
        Detalles = inscripcion.InscripcionDetalles.Select(x => x.ToDTO()).ToList()
    };

    public static InscripcionDetalleDTO ToDTO(this InscripcionDetalle detalle) => new()
    {
        Id = detalle.Id,
        TallerId = detalle.TallerId,
        TallerNombre = detalle.Taller?.Nombre ?? string.Empty,
        Precio = detalle.Precio
    };

    public static Inscripcion ToEntity(this CreateInscripcionDTO dto, List<Taller> talleres) => new()
    {
        NinoId = dto.NinoId,
        PeriodoId = dto.PeriodoId,
        FechaRegistro = DateTime.Now,
        Total = talleres.Sum(x => x.CostoMensual),
        Pagada = false,
        FechaVencimiento = dto.FechaVencimiento,
        InscripcionDetalles = talleres.Select(x => new InscripcionDetalle
        {
            TallerId = x.Id,
            Precio = x.CostoMensual
        }).ToList()
    };
}
