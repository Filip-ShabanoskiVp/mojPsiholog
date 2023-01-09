using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mojPsihologApp.Models;
public partial class Psiholog
{

    [Display(Name = "Корисничко име")]
    public string Korisnickoime { get; set; } = null!;

    public virtual ICollection<DavaOcenka> DavaOcenkas { get; } = new List<DavaOcenka>();

    public virtual Korisnik KorisnickoimeNavigation { get; set; } = null!;

    public virtual ICollection<Termin> Termins { get; } = new List<Termin>();

    public virtual ICollection<Usluga> IdUslugas { get; } = new List<Usluga>();

    public virtual ICollection<PsihologNudiUsluga> PsihologNudiUslugas { get; } = new List<PsihologNudiUsluga>();
}
