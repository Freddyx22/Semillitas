using Microsoft.EntityFrameworkCore;
using Semillitas.Api.Entities;
using Semillitas.Api.Mappers;
using Semillitas.Api.Repositories;
using Semillitas.Core.DTOs;

namespace Semillitas.Api.Services;

public class InscripcionService
{
    private readonly Repository<Inscripcion> _inscripciones;
    private readonly Repository<Taller> _talleres;

    public InscripcionService(Repository<Inscripcion> inscripciones, Repository<Taller> talleres)
    {
        _inscripciones = inscripciones;
        _talleres = talleres;
    }

    private IQueryable<Inscripcion> ConDetalle() => _inscripciones.Query()
        .Include(x => x.Nino)
        .Include(x => x.Periodo)
        .Include(x =>x.InscripcionDetalles).ThenInclude(x => x.Taller);

    public List<InscripcionDTO> GetByPeriodo(int periodoId) => ConDetalle()
        .Where(x => x.PeriodoId == periodoId).OrderBy(x => x.Nino.Nombre)
        .ToList().Select(x => x.ToDTO()).ToList();

    public InscripcionDTO? GetById(int id) => ConDetalle()
        .Where(x => x.Id == id).ToList().Select(x => x.ToDTO()).FirstOrDefault();

    public List<InscripcionDTO> GetVencidas()
    {
        var hoy = DateOnly.FromDateTime(DateTime.Today);
        return ConDetalle()
            .Where(x => !x.Pagada && x.FechaVencimiento < hoy).OrderBy(x => x.FechaVencimiento)
            .ToList().Select(x => x.ToDTO()).ToList();
    }

    public InscripcionDTO? Create(CreateInscripcionDTO dto)
    {
        var talleres = _talleres.Query().Where(x => dto.TalleresIds.Contains(x.Id)).ToList();
        if (talleres.Count != dto.TalleresIds.Count) return null;
        var inscripcion = _inscripciones.Add(dto.ToEntity(talleres));
        return GetById(inscripcion.Id);
    }

    public InscripcionDTO? MarcarPago(int id, bool pagada)
    {
        var inscripcion = _inscripciones.GetById(id);
        if (inscripcion is null) return null;
        inscripcion.Pagada = pagada;
        inscripcion.FechaPago = pagada ? DateTime.Now : null;
        _inscripciones.Update(inscripcion);
        return GetById(id);
    }

    public bool Delete(int id)
    {
        var inscripcion = _inscripciones.GetById(id);
        if (inscripcion is null) return false;
        _inscripciones.Delete(inscripcion);
        return true;
    }
}
