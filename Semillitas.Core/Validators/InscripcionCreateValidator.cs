using Semillitas.Core.DTOs;

namespace Semillitas.Core.Validators;

public class InscripcionCreateValidator
{
    public virtual ValidationResult IsValid(CreateInscripcionDTO dto)
    {
        ValidationResult result = new();
        if (dto.NinoId <= 0) result.Errors.Add("Debe indicar un niño válido.");
        if (dto.PeriodoId <= 0) result.Errors.Add("Debe indicar un período válido.");
        if (dto.FechaVencimiento == default) result.Errors.Add("La fecha de vencimiento es obligatoria.");
        if (dto.TalleresIds is null || dto.TalleresIds.Count == 0) result.Errors.Add("Debe seleccionar al menos un taller.");
        else
        {
            if (dto.TalleresIds.Any(x => x <= 0)) result.Errors.Add("Alguno de los talleres indicados no es válido.");
            if (dto.TalleresIds.Distinct().Count() != dto.TalleresIds.Count) result.Errors.Add("No puede repetir un taller en la misma inscripción.");
        }
        return result;
    }
}
