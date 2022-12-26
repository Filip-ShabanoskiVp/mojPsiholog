using System;
using System.Collections.Generic;

namespace mojPsihologApp.Models;

public partial class Termin
{
    public int IdTermin { get; set; }

    public string Korisnickoime { get; set; } = null!;

    public double? Vreme { get; set; }

    public string? Grad { get; set; }

    public DateTime? Datum { get; set; }

    public virtual Psiholog KorisnickoimeNavigation { get; set; } = null!;

    public virtual ICollection<Plaka> Plakas { get; } = new List<Plaka>();

    public virtual ICollection<Sesija> IdSesijas { get; } = new List<Sesija>();

    public virtual ICollection<Pacient> Korisnickoimes { get; } = new List<Pacient>();
}
