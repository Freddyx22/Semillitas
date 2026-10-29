using Semillitas.Core.DTOs;

namespace Semillitas.Core.Validators;

public class CategoriaCreateValidator
{
    public virtual ValidationResult IsValid(CreateCategoriaDTO dto)
    {
        ValidationResult result = new();
        if (string.IsNullOrWhiteSpace(dto.Nombre)) result.Errors.Add("El nombre de la categoría es obligatorio.");
        else if (dto.Nombre.Trim().Length > 100) result.Errors.Add("El nombre de la categoría no puede exceder 100 caracteres.");
        return result;
    }
}
