using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace mojPsihologApp.Models;

public partial class DavaOcenka
{
    [Display(Name = "Корисничко име пациент")]
    public string Korisnickoimepacient { get; set; } = null!;

    [Display(Name = "Корисничко име психолог")]
    public string Korisnickoimepsiholog { get; set; } = null!;

    [Display(Name = "Оценка")]
    public int IdOcenka { get; set; }

    public virtual Ocenka IdOcenkaNavigation { get; set; } = null!;

    public virtual Pacient KorisnickoimepacientNavigation { get; set; } = null!;

    public virtual Psiholog KorisnickoimepsihologNavigation { get; set; } = null!;
}
