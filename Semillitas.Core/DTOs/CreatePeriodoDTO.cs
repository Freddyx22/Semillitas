namespace Semillitas.Core.DTOs;

public class CreatePeriodoDTO
{
    public string Nombre { get; set; } = string.Empty;
    public DateOnly FechaInicio { get; set; }
    public DateOnly FechaFin { get; set; }
}
