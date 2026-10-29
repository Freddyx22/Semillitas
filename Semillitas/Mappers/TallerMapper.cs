using Semillitas.Api.Entities;
using Semillitas.Core.DTOs;

namespace Semillitas.Api.Mappers;

public static class TallerMapper
{
    public static TallerDTO ToDTO(this Taller taller) => new()
    {
        Id = taller.Id,
        Nombre = taller.Nombre,
        CategoriaId = taller.CategoriaId,
        Instructor = taller.Instructor,
        DiaSemana = taller.DiaSemana,
        HoraInicio = taller.HoraInicio,
        HoraFin = taller.HoraFin,
        CupoMaximo = taller.CupoMaximo,
        CostoMensual = taller.CostoMensual,
        Activo = (bool)taller.Activo,
        CategoriaNombre = taller.Categoria?.Nombre ?? string.Empty
    };

    public static TallerOcupacionDTO ToOcupacionDTO(this Taller taller, int inscritos) => new()
    {
        TallerId = taller.Id,
        TallerNombre = taller.Nombre,
        CategoriaNombre = taller.Categoria?.Nombre ?? string.Empty,
        DiaSemana = taller.DiaSemana,
        HoraInicio = taller.HoraInicio,
        HoraFin = taller.HoraFin,
        CupoMaximo = taller.CupoMaximo,
        Inscritos = inscritos,
        CupoDisponible = taller.CupoMaximo - inscritos
    };

    public static Taller ToEntity(this CreateTallerDTO dto) => new()
    {
        Nombre = dto.Nombre.Trim(),
        CategoriaId = dto.CategoriaId,
        Instructor = dto.Instructor.Trim(),
        DiaSemana = dto.DiaSemana,
        HoraInicio = dto.HoraInicio,
        HoraFin = dto.HoraFin,
        CupoMaximo = dto.CupoMaximo,
        CostoMensual = dto.CostoMensual,
        Activo = dto.Activo
    };

    public static void ApplyTo(this EditTallerDTO dto, Taller taller)
    {
        taller.Nombre = dto.Nombre.Trim();
        taller.CategoriaId = dto.CategoriaId;
        taller.Instructor = dto.Instructor.Trim();
        taller.DiaSemana = dto.DiaSemana;
        taller.HoraInicio = dto.HoraInicio;
        taller.HoraFin = dto.HoraFin;
        taller.CupoMaximo = dto.CupoMaximo;
        taller.CostoMensual = dto.CostoMensual;
        taller.Activo = dto.Activo;
    }
}
