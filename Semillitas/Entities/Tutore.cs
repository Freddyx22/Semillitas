using System;
using System.Collections.Generic;

namespace Semillitas.Api.Entities;

public partial class Tutor
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? Email { get; set; }

    public virtual ICollection<Nino> Ninos { get; set; } = new List<Nino>();
}
