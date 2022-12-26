using System;
using System.Collections.Generic;

namespace mojPsihologApp.Models;

public partial class Korisnik
{
    public string Korisnickoime { get; set; } = null!;

    public string? Ime { get; set; }

    public string? Prezime { get; set; }

    public string? Uloga { get; set; }

    public string? Telefon { get; set; }

    public string? Meil { get; set; }

    public string? Lozinka { get; set; }

    public string? Ulica { get; set; }

    public string? Grad { get; set; }

    public virtual Pacient? Pacient { get; set; }

    public virtual Psiholog? Psiholog { get; set; }
}
