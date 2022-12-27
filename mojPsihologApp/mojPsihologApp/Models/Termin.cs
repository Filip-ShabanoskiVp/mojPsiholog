using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace mojPsihologApp.Models;

public partial class Termin
{
    public int IdTermin { get; set; }

    [DisplayName("Корисничко име")]
    public string Korisnickoime { get; set; } = null!;

    [DisplayName("Време")]
    public double? Vreme { get; set; }

    [DisplayName("Град")]
    public string? Grad { get; set; }

    [DisplayName("Датум")]
    public DateTime? Datum { get; set; }

    public virtual Psiholog KorisnickoimeNavigation { get; set; } = null!;

    public virtual ICollection<Plaka> Plakas { get; } = new List<Plaka>();

    public virtual ICollection<Sesija> IdSesijas { get; } = new List<Sesija>();

    public virtual ICollection<Pacient> Korisnickoimes { get; } = new List<Pacient>();
}
