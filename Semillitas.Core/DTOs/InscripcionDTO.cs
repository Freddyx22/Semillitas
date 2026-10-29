namespace Semillitas.Core.DTOs;

public class InscripcionDTO
{
    public int Id { get; set; }
    public int NinoId { get; set; }
    public string NinoNombre { get; set; } = string.Empty;
    public int PeriodoId { get; set; }
    public string PeriodoNombre { get; set; } = string.Empty;
    public DateTime FechaRegistro { get; set; }
    public decimal Total { get; set; }
    public bool Pagada { get; set; }
    public DateTime? FechaPago { get; set; }
    public DateOnly FechaVencimiento { get; set; }
    public bool Vencida { get; set; }
    public List<InscripcionDetalleDTO> Detalles { get; set; } = new();
}
