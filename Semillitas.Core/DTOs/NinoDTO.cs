namespace Semillitas.Core.DTOs;

public class NinoDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public DateOnly FechaNacimiento { get; set; }
    public int TutorId { get; set; }
    public string TutorNombre { get; set; } = string.Empty;
}
