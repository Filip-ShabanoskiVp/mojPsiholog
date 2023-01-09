namespace mojPsihologApp.Models
{
    public class PsihologNudiUsluga
    {
        public string korisnickoime { get; set; } = null!;

        public int idUsluga { get; set; }

        public virtual Psiholog KorisnickoimeNavigation { get; set; } = null!;

        public virtual Usluga IdUslugaNavigation { get; set; } = null!;
    }
}
