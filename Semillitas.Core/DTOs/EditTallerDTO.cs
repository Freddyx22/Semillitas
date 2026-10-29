namespace Semillitas.Core.DTOs;

public class EditTallerDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int CategoriaId { get; set; }
    public string Instructor { get; set; } = string.Empty;
    public int DiaSemana { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }
    public int CupoMaximo { get; set; }
    public decimal CostoMensual { get; set; }
    public bool Activo { get; set; }
}
