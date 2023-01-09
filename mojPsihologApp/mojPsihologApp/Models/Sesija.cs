using System;
using System.Collections.Generic;

namespace mojPsihologApp.Models;

public partial class Sesija
{
    public int IdSesija { get; set; }

    public double? Vremetraenje { get; set; }

    public string? Ulica { get; set; }

    public string? Broj { get; set; }

    public virtual ICollection<Termin> IdTermins { get; } = new List<Termin>();

    public virtual ICollection<SesijaSeOdrzuvaVoTermin> SesijaSeOdrzuvaVoTermins { get; } = new List<SesijaSeOdrzuvaVoTermin>();
}
