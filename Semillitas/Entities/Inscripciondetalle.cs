using System;
using System.Collections.Generic;

namespace Semillitas.Api.Entities;

public partial class InscripcionDetalle
{
    public int Id { get; set; }

    public int InscripcionId { get; set; }

    public int TallerId { get; set; }

    public decimal Precio { get; set; }

    public virtual Inscripcion Inscripcion { get; set; } = null!;

    public virtual Taller Taller { get; set; } = null!;
}
