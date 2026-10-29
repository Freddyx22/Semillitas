using System;
using System.Collections.Generic;

namespace Semillitas.Api.Entities;

public partial class Taller
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int CategoriaId { get; set; }

    public string Instructor { get; set; } = null!;

    public int DiaSemana { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public int CupoMaximo { get; set; }

    public decimal CostoMensual { get; set; }

    public bool? Activo { get; set; }

    public virtual Categoria Categoria { get; set; } = null!;

    public virtual ICollection<InscripcionDetalle> InscripcionDetalles { get; set; } = new List<InscripcionDetalle>();
}
