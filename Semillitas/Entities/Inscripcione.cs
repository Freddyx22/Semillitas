using System;
using System.Collections.Generic;

namespace Semillitas.Api.Entities;

public partial class Inscripcion
{
    public int Id { get; set; }

    public int NinoId { get; set; }

    public int PeriodoId { get; set; }

    public DateTime FechaRegistro { get; set; }

    public decimal Total { get; set; }

    public bool Pagada { get; set; }

    public DateTime? FechaPago { get; set; }

    public DateOnly FechaVencimiento { get; set; }

    public virtual ICollection<InscripcionDetalle> InscripcionDetalles { get; set; } = new List<InscripcionDetalle>();

    public virtual Nino Nino { get; set; } = null!;

    public virtual Periodo Periodo { get; set; } = null!;
}
