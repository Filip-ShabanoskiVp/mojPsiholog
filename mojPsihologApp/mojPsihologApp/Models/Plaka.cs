using System;
using System.Collections.Generic;

namespace mojPsihologApp.Models;

public partial class Plaka
{
    public string Korisnickoime { get; set; } = null!;

    public int Id { get; set; }

    public int IdTermin { get; set; }

    public virtual Plakanje IdNavigation { get; set; } = null!;

    public virtual Termin IdTerminNavigation { get; set; } = null!;

    public virtual Pacient KorisnickoimeNavigation { get; set; } = null!;
}
