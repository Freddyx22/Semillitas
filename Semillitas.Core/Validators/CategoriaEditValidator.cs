using Semillitas.Core.DTOs;

namespace Semillitas.Core.Validators;

public class CategoriaEditValidator
{
    public virtual ValidationResult IsValid(EditCategoriaDTO dto)
    {
        ValidationResult result = new();
        if (dto.Id <= 0) result.Errors.Add("Debe indicar una categoría válida para editar.");
        if (string.IsNullOrWhiteSpace(dto.Nombre)) result.Errors.Add("El nombre de la categoría es obligatorio.");
        else if (dto.Nombre.Trim().Length > 100) result.Errors.Add("El nombre de la categoría no puede exceder 100 caracteres.");
        return result;
    }
}
