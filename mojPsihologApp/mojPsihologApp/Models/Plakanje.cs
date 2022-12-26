using System;
using System.Collections.Generic;

namespace mojPsihologApp.Models;

public partial class Plakanje
{
    public int Id { get; set; }

    public int? Suma { get; set; }

    public virtual ICollection<Plaka> Plakas { get; } = new List<Plaka>();
}
