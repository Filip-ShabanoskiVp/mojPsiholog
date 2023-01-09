using System.ComponentModel.DataAnnotations.Schema;

namespace mojPsihologApp.Models
{
    public class SesijaSeOdrzuvaVoTermin
    {
        public int idSesija { get; set; }

        public int idTermin { get; set; }

        public virtual Sesija IdSesijaNavigation { get; set; } = null!;

        public virtual Termin IdTerminNavigation { get; set; } = null!;
    }
}
