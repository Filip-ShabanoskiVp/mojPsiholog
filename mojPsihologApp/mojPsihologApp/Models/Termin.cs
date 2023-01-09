using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mojPsihologApp.Models;

public partial class Termin
{
    public int IdTermin { get; set; }

    [Display(Name = "Корисничко име")]
    public string Korisnickoime { get; set; } = null!;


    [Display(Name = "Време")]
    public double? Vreme { get; set; }


    [Display(Name = "Град")]
    public string? Grad { get; set; }


    [Display(Name = "Датум")]
    public DateTime? Datum { get; set; }


    public virtual Psiholog KorisnickoimeNavigation { get; set; } = null!;

    public virtual ICollection<Plaka> Plakas { get; } = new List<Plaka>();

    public virtual ICollection<Sesija> IdSesijas { get; } = new List<Sesija>();

    public virtual ICollection<Pacient> Korisnickoimes { get; } = new List<Pacient>();

    public virtual ICollection<PacientotZakazuvaTermin> PacientotZakazuvaTermins { get; } = new List<PacientotZakazuvaTermin>();


    public virtual ICollection<SesijaSeOdrzuvaVoTermin> SesijaSeOdrzuvaVoTermins { get; } = new List<SesijaSeOdrzuvaVoTermin>();
}
