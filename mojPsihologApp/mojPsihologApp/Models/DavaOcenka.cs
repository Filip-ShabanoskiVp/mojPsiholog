using System;
using System.Collections.Generic;

namespace mojPsihologApp.Models;

public partial class DavaOcenka
{
    public string Korisnickoimepacient { get; set; } = null!;

    public string Korisnickoimepsiholog { get; set; } = null!;

    public int IdOcenka { get; set; }

    public virtual Ocenka IdOcenkaNavigation { get; set; } = null!;

    public virtual Pacient KorisnickoimepacientNavigation { get; set; } = null!;

    public virtual Psiholog KorisnickoimepsihologNavigation { get; set; } = null!;
}
