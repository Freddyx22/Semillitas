namespace Semillitas.Core.DTOs;

public class CreateTutorDTO
{
    public string Nombre { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string? Email { get; set; }
}
