using System;
using System.Collections.Generic;

namespace mojPsihologApp.Models;

public partial class Ocenka
{
    public int IdOcenka { get; set; }

    public int? Ocenka1 { get; set; }

    public virtual ICollection<DavaOcenka> DavaOcenkas { get; } = new List<DavaOcenka>();
}
