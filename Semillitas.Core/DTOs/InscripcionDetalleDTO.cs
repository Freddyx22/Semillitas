namespace Semillitas.Core.DTOs;

public class InscripcionDetalleDTO
{
    public int Id { get; set; }
    public int TallerId { get; set; }
    public string TallerNombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
}
