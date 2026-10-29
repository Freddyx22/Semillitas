using System;
using System.Collections.Generic;

namespace Semillitas.Api.Entities;

public partial class Categoria
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Taller> Talleres { get; set; } = new List<Taller>();
}
