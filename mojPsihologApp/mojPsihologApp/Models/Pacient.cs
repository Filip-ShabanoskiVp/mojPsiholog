using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace mojPsihologApp.Models;

public partial class Pacient
{
    public string Korisnickoime { get; set; } = null!;

    public virtual ICollection<DavaOcenka> DavaOcenkas { get; } = new List<DavaOcenka>();

    public virtual Korisnik KorisnickoimeNavigation { get; set; } = null!;

    public virtual ICollection<Plaka> Plakas { get; } = new List<Plaka>();

    public virtual ICollection<Termin> IdTermins { get; } = new List<Termin>();

    public virtual ICollection<Usluga> IdUslugas { get; } = new List<Usluga>();

    public virtual ICollection<PacientotZakazuvaTermin> PacientotZakazuvaTermins { get; } = new List<PacientotZakazuvaTermin>();

    public virtual ICollection<PacientRazgleduvaUsluga> PacientRazgleduvaUslugas { get; } = new List<PacientRazgleduvaUsluga>();
}
