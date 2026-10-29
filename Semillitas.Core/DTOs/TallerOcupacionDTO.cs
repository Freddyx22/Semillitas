namespace Semillitas.Core.DTOs;

public class TallerOcupacionDTO
{
    public int TallerId { get; set; }
    public string TallerNombre { get; set; } = string.Empty;
    public string CategoriaNombre { get; set; } = string.Empty;
    public int DiaSemana { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }
    public int CupoMaximo { get; set; }
    public int Inscritos { get; set; }
    public int CupoDisponible { get; set; }
}
