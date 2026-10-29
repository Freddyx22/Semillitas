namespace Semillitas.Core.DTOs;

public class RankingTallerDTO
{
    public int Posicion { get; set; }
    public int TallerId { get; set; }
    public string TallerNombre { get; set; } = string.Empty;
    public string CategoriaNombre { get; set; } = string.Empty;
    public int TotalInscritos { get; set; }
}
