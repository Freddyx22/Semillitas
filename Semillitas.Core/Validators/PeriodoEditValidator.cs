using Semillitas.Core.DTOs;

namespace Semillitas.Core.Validators;

public class PeriodoEditValidator
{
    public virtual ValidationResult IsValid(EditPeriodoDTO dto)
    {
        ValidationResult result = new();
        if (dto.Id <= 0) result.Errors.Add("Debe indicar un período válido para editar.");
        if (string.IsNullOrWhiteSpace(dto.Nombre)) result.Errors.Add("El nombre del período es obligatorio.");
        else if (dto.Nombre.Trim().Length > 50) result.Errors.Add("El nombre del período no puede exceder 50 caracteres.");
        if (dto.FechaInicio == default) result.Errors.Add("La fecha de inicio es obligatoria.");
        if (dto.FechaFin == default) result.Errors.Add("La fecha de fin es obligatoria.");
        if (dto.FechaInicio != default && dto.FechaFin != default && dto.FechaFin < dto.FechaInicio)
            result.Errors.Add("La fecha de fin no puede ser anterior a la fecha de inicio.");
        return result;
    }
}
