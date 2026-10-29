using Microsoft.EntityFrameworkCore;
using Semillitas.Api.Entities;
using Semillitas.Api.Mappers;
using Semillitas.Api.Repositories;
using Semillitas.Core.DTOs;

namespace Semillitas.Api.Services;

public class PeriodoService
{
    private readonly Repository<Periodo> _periodos;

    public PeriodoService(Repository<Periodo> periodos)
    {
        _periodos = periodos;
    }

    public List<PeriodoDTO> GetAll() => _periodos.Query()
        .OrderByDescending(x => x.FechaInicio).Select(x => x.ToDTO()).ToList();

    public PeriodoDTO? GetById(int id) => _periodos.Query()
        .Where(x => x.Id == id).Select(x => x.ToDTO()).FirstOrDefault();

    public PeriodoDTO? Create(CreatePeriodoDTO dto) => _periodos.Add(dto.ToEntity()).ToDTO();

    public PeriodoDTO? Update(EditPeriodoDTO dto)
    {
        var periodo = _periodos.GetById(dto.Id);
        if (periodo is null) return null;
        dto.ApplyTo(periodo);
        _periodos.Update(periodo);
        return periodo.ToDTO();
    }

    public bool Delete(int id)
    {
        var periodo = _periodos.GetById(id);
        if (periodo is null) return false;
        _periodos.Delete(periodo);
        return true;
    }
}
