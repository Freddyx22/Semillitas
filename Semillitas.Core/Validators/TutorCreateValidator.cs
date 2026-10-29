using Semillitas.Core.DTOs;

namespace Semillitas.Core.Validators;

public class TutorCreateValidator
{
    public virtual ValidationResult IsValid(CreateTutorDTO dto)
    {
        ValidationResult result = new();
        if (string.IsNullOrWhiteSpace(dto.Nombre)) result.Errors.Add("El nombre del tutor es obligatorio.");
        else if (dto.Nombre.Trim().Length > 150) result.Errors.Add("El nombre del tutor no puede exceder 150 caracteres.");
        if (string.IsNullOrWhiteSpace(dto.Telefono)) result.Errors.Add("El teléfono es obligatorio.");
        else if (dto.Telefono.Trim().Length > 20) result.Errors.Add("El teléfono no puede exceder 20 caracteres.");
        if (!string.IsNullOrWhiteSpace(dto.Email))
        {
            if (!dto.Email.Contains('@')) result.Errors.Add("El correo electrónico no es válido.");
            else if (dto.Email.Trim().Length > 150) result.Errors.Add("El correo electrónico no puede exceder 150 caracteres.");
        }
        return result;
    }
}
