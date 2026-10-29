using Semillitas.Core.DTOs;

namespace Semillitas.Core.Validators;

public class TallerCreateValidator
{
    public virtual ValidationResult IsValid(CreateTallerDTO dto)
    {
        ValidationResult result = new();
        if (string.IsNullOrWhiteSpace(dto.Nombre)) result.Errors.Add("El nombre del taller es obligatorio.");
        else if (dto.Nombre.Trim().Length > 100) result.Errors.Add("El nombre del taller no puede exceder 100 caracteres.");
        if (dto.CategoriaId <= 0) result.Errors.Add("Debe indicar una categoría válida.");
        if (string.IsNullOrWhiteSpace(dto.Instructor)) result.Errors.Add("El instructor es obligatorio.");
        else if (dto.Instructor.Trim().Length > 150) result.Errors.Add("El nombre del instructor no puede exceder 150 caracteres.");
        if (dto.DiaSemana < 1 || dto.DiaSemana > 7) result.Errors.Add("El día de la semana debe estar entre 1 (lunes) y 7 (domingo).");
        if (dto.HoraFin <= dto.HoraInicio) result.Errors.Add("La hora de fin debe ser mayor que la hora de inicio.");
        if (dto.CupoMaximo <= 0) result.Errors.Add("El cupo máximo debe ser mayor que cero.");
        if (dto.CostoMensual < 0) result.Errors.Add("El costo mensual no puede ser negativo.");
        return result;
    }
}
