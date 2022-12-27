using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace mojPsihologApp.Models;

public partial class Korisnik
{
    [DisplayName("Корисничко име")]
    public string Korisnickoime { get; set; } = null!;

    [DisplayName("Име")]
    public string? Ime { get; set; }

    [DisplayName("Презиме")]
    public string? Prezime { get; set; }

    [DisplayName("Улога")]
    public string? Uloga { get; set; }

    [DisplayName("Телефон")]
    public string? Telefon { get; set; }

    [DisplayName("Е-маил")]
    public string? Meil { get; set; }

    [DisplayName("Лозинка")]
    public string? Lozinka { get; set; }

    [DisplayName("Улица")]
    public string? Ulica { get; set; }

    [DisplayName("Град")]
    public string? Grad { get; set; }

    public virtual Pacient? Pacient { get; set; }

    public virtual Psiholog? Psiholog { get; set; }
}
