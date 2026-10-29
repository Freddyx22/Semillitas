using System;
using System.Collections.Generic;

namespace Semillitas.Api.Entities;

public partial class Nino
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public int TutorId { get; set; }

    public virtual ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();

    public virtual Tutor Tutor { get; set; } = null!;
}
