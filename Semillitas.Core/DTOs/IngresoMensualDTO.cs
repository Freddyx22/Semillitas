namespace Semillitas.Core.DTOs;

public class IngresoMensualDTO
{
    public int PeriodoId { get; set; }
    public string PeriodoNombre { get; set; } = string.Empty;
    public int TotalInscripciones { get; set; }
    public decimal TotalFacturado { get; set; }
    public decimal TotalCobrado { get; set; }
    public decimal TotalPendiente { get; set; }
}
