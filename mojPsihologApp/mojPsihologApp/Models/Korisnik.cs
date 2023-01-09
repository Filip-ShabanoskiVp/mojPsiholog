
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace mojPsihologApp.Models;

public partial class Korisnik
{
    [DisplayName("Корисничко име")]
    [Required(ErrorMessage = "Внеси корисничко име")]
    public string Korisnickoime { get; set; } = null!;

    [DisplayName("Име")]
    [Required(ErrorMessage = "Внеси име")]
    public string? Ime { get; set; }

    [DisplayName("Презиме")]
    [Required(ErrorMessage = "Внеси презиме")]
    public string? Prezime { get; set; }

    [DisplayName("Улога")]
    [Required(ErrorMessage = "Внеси улога")]

    public string? Uloga { get; set; }

    [DisplayName("Телефонски број")]
    [Required(ErrorMessage = "Внеси телефонски број")]
    [RegularExpression(@"^[0-9]\d{2}\.\d{3}\.\d{3}",ErrorMessage ="Погрешен формат на телефонски број!")]
    public string? Telefon { get; set; }

    [DisplayName("Емаил")]
    [Required(ErrorMessage = "Внеси емаил")]

    public string? Meil { get; set; }

    [DisplayName("Лозинка")]
    [Required(ErrorMessage = "Внеси лозинка")]

    public string? Lozinka { get; set; }

    [DisplayName("Улица")]
    [Required(ErrorMessage = "Внеси улица")]

    public string? Ulica { get; set; }

    [DisplayName("Град")]
    [Required(ErrorMessage = "Внеси град")]

    public string? Grad { get; set; }

    public virtual Pacient? Pacient { get; set; }

    public virtual Psiholog? Psiholog { get; set; }
}
