using Microsoft.EntityFrameworkCore;
using Semillitas.Api.Entities;
using Semillitas.Api.Mappers;
using Semillitas.Api.Repositories;
using Semillitas.Core.DTOs;

namespace Semillitas.Api.Services;

public class ReporteService
{
    private readonly Repository<Taller> _talleres;
    private readonly Repository<InscripcionDetalle> _detalles;
    private readonly Repository<Inscripcion> _inscripciones;
    private readonly Repository<Periodo> _periodos;

    public ReporteService(Repository<Taller> talleres, Repository<InscripcionDetalle> detalles,
        Repository<Inscripcion> inscripciones, Repository<Periodo> periodos)
    {
        _talleres = talleres;
        _detalles = detalles;
        _inscripciones = inscripciones;
        _periodos = periodos;
    }

    public List<TallerOcupacionDTO> GetInscritosPorTaller(int periodoId)
    {
        var inscritos = _detalles.Query()
            .Where(x => x.Inscripcion.PeriodoId == periodoId)
            .GroupBy(x => x.TallerId)
            .Select(g => new { TallerId = g.Key, Total = g.Count() })
            .ToDictionary(x => x.TallerId, x => x.Total);

        return _talleres.Query().Include(x => x.Categoria)
            .Where(x => (bool)x.Activo).OrderBy(x => x.Nombre)
            .ToList().Select(x => x.ToOcupacionDTO(inscritos.GetValueOrDefault(x.Id))).ToList();
    }

    public IngresoMensualDTO? GetIngresoMensual(int periodoId)
    {
        var periodo = _periodos.GetById(periodoId);
        if (periodo is null) return null;

        var inscripciones = _inscripciones.Query().Where(x => x.PeriodoId == periodoId).ToList();
        var facturado = inscripciones.Sum(x => x.Total);
        var cobrado = inscripciones.Where(x => x.Pagada).Sum(x => x.Total);

        return new IngresoMensualDTO
        {
            PeriodoId = periodo.Id,
            PeriodoNombre = periodo.Nombre,
            TotalInscripciones = inscripciones.Count,
            TotalFacturado = facturado,
            TotalCobrado = cobrado,
            TotalPendiente = facturado - cobrado
        };
    }

    public List<RankingTallerDTO> GetRankingTalleres(int? periodoId)
    {
        var detalles = _detalles.Query();
        if (periodoId.HasValue)
            detalles = detalles.Where(x => x.Inscripcion.PeriodoId == periodoId.Value);

        var conteo = detalles
            .GroupBy(x => x.TallerId)
            .Select(g => new { TallerId = g.Key, Total = g.Count() })
            .OrderByDescending(x => x.Total).ThenBy(x => x.TallerId)
            .ToList();

        var talleres = _talleres.Query().Include(x => x.Categoria).ToDictionary(x => x.Id);

        return conteo.Select((x, i) => new RankingTallerDTO
        {
            Posicion = i + 1,
            TallerId = x.TallerId,
            TallerNombre = talleres[x.TallerId].Nombre,
            CategoriaNombre = talleres[x.TallerId].Categoria.Nombre,
            TotalInscritos = x.Total
        }).ToList();
    }
}
