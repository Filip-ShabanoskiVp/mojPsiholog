using System.ComponentModel;

namespace mojPsihologApp.Models
{
    public class PacientotZakazuvaTermin
    {
        [DisplayName("Корисничко име на пациент")]
        public string korisnickoime { get; set; }

        [DisplayName("Ид термин")]
        public int idTermin { get; set; }


        public virtual Pacient KorisnickoimeNavigation { get; set; } = null!;


        public virtual Termin IdTerminNavigation { get; set; } = null!;
    }
}
