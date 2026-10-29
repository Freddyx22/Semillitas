using Semillitas.Api.Entities;
using Semillitas.Api.Repositories;
using Semillitas.Core.DTOs;
using Semillitas.Core.Validators;

namespace Semillitas.Api.Validators;

public class NewInscripcionValidator : InscripcionCreateValidator
{
    private readonly Repository<Nino> _ninos;
    private readonly Repository<Periodo> _periodos;
    private readonly Repository<Taller> _talleres;
    private readonly Repository<Inscripcion> _inscripciones;
    private readonly Repository<InscripcionDetalle> _detalles;

    public NewInscripcionValidator(Repository<Nino> ninos, Repository<Periodo> periodos, Repository<Taller> talleres,
        Repository<Inscripcion> inscripciones, Repository<InscripcionDetalle> detalles)
    {
        _ninos = ninos;
        _periodos = periodos;
        _talleres = talleres;
        _inscripciones = inscripciones;
        _detalles = detalles;
    }

    public override ValidationResult IsValid(CreateInscripcionDTO dto)
    {
        var result = base.IsValid(dto);
        if (!result.IsValid) return result;

        if (_ninos.GetById(dto.NinoId) is null)
            result.Errors.Add("El niño indicado no existe.");

        var periodo = _periodos.GetById(dto.PeriodoId);
        if (periodo is null)
            result.Errors.Add("El período indicado no existe.");
        // Control 3: no se puede inscribir en un período que ya empezó.
        else if (periodo.FechaInicio <= DateOnly.FromDateTime(DateTime.Today))
            result.Errors.Add("No se puede inscribir en un período que ya inició.");

        if (_inscripciones.Query().Any(x => x.NinoId == dto.NinoId && x.PeriodoId == dto.PeriodoId))
            result.Errors.Add("El niño ya tiene una inscripción en este período.");

        var talleres = _talleres.Query().Where(x => dto.TalleresIds.Contains(x.Id)).ToList();
        if (talleres.Count != dto.TalleresIds.Count)
        {
            result.Errors.Add("Alguno de los talleres indicados no existe.");
            return result;
        }

        foreach (var taller in talleres)
        {
            if ((bool)!taller.Activo)
                result.Errors.Add($"El taller {taller.Nombre} no está activo.");

            // Control 1: no se puede inscribir en un taller sin cupo.
            var inscritos = _detalles.Query().Count(x => x.TallerId == taller.Id && x.Inscripcion.PeriodoId == dto.PeriodoId);
            if (inscritos >= taller.CupoMaximo)
                result.Errors.Add($"El taller {taller.Nombre} no tiene cupo disponible.");
        }

        // Control 2: no se puede inscribir en dos talleres con horario solapado.
        for (int i = 0; i < talleres.Count; i++)
        {
            for (int j = i + 1; j < talleres.Count; j++)
            {
                var a = talleres[i];
                var b = talleres[j];
                if (a.DiaSemana == b.DiaSemana && a.HoraInicio < b.HoraFin && b.HoraInicio < a.HoraFin)
                    result.Errors.Add($"Los talleres {a.Nombre} y {b.Nombre} tienen horario solapado.");
            }
        }
        return result;
    }
}
