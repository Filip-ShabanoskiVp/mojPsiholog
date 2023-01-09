using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace mojPsihologApp.Models;

public partial class Ocenka
{
    public int IdOcenka { get; set; }

    [Display(Name = "Оценка")]
    public int? Ocenka1 { get; set; }
   // public int Ocenka1 { get; set; }

    public virtual ICollection<DavaOcenka> DavaOcenkas { get; } = new List<DavaOcenka>();
}
