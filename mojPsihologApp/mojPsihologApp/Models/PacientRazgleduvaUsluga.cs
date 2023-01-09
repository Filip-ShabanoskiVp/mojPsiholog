using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace mojPsihologApp.Models
{
    public class PacientRazgleduvaUsluga
    {

        public string korisnickoime { get; set; } = null!;

        public int idUsluga { get; set; }

        public virtual Pacient KorisnickoimeNavigation { get; set; } = null!;

        public virtual Usluga IdUslugaNavigation { get; set; } = null!;

    }
}
