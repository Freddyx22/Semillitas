using Semillitas.Core.DTOs;

namespace Semillitas.Core.Validators;

public class NinoCreateValidator
{
    public virtual ValidationResult IsValid(CreateNinoDTO dto)
    {
        ValidationResult result = new();
        if (string.IsNullOrWhiteSpace(dto.Nombre)) result.Errors.Add("El nombre del niño es obligatorio.");
        else if (dto.Nombre.Trim().Length > 150) result.Errors.Add("El nombre del niño no puede exceder 150 caracteres.");
        if (dto.FechaNacimiento == default) result.Errors.Add("La fecha de nacimiento es obligatoria.");
        else if (dto.FechaNacimiento > DateOnly.FromDateTime(DateTime.Today)) result.Errors.Add("La fecha de nacimiento no puede ser futura.");
        if (dto.TutorId <= 0) result.Errors.Add("Debe indicar un tutor válido.");
        return result;
    }
}
