namespace Semillitas.Core.DTOs;

public class CreateNinoDTO
{
    public string Nombre { get; set; } = string.Empty;
    public DateOnly FechaNacimiento { get; set; }
    public int TutorId { get; set; }
}
