using System;
using System.Collections.Generic;

namespace mojPsihologApp.Models;

public partial class Usluga
{
    public int IdUsluga { get; set; }

    public string? Ime { get; set; }

    public string? Opis { get; set; }

    public virtual ICollection<Pacient> Korisnickoimes { get; } = new List<Pacient>();

    public virtual ICollection<Psiholog> KorisnickoimesNavigation { get; } = new List<Psiholog>();

    public virtual ICollection<PsihologNudiUsluga> PsihologNudiUslugas { get; } = new List<PsihologNudiUsluga>();

    public virtual ICollection<PacientRazgleduvaUsluga> PacientRazgleduvaUslugas { get; } = new List<PacientRazgleduvaUsluga>();
}
