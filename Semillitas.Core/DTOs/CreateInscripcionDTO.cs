namespace Semillitas.Core.DTOs;

public class CreateInscripcionDTO
{
    public int NinoId { get; set; }
    public int PeriodoId { get; set; }
    public DateOnly FechaVencimiento { get; set; }
    public List<int> TalleresIds { get; set; } = new();
}
