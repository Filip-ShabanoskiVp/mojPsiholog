using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using mojPsihologApp.Models;

namespace mojPsihologApp.mojPsihologDbContext;

public partial class MojPsihologContext : DbContext
{
    public MojPsihologContext()
    {
    }

    public MojPsihologContext(DbContextOptions<MojPsihologContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DavaOcenka> DavaOcenkas { get; set; }

    public virtual DbSet<KorisniciCijSessiSeOdrzalePred1GodinaISegasnataImeEF> KorisniciCijSessiSeOdrzalePred1GodinaISegasnataImeEFs { get; set; }

    public virtual DbSet<Korisnik> Korisniks { get; set; }

    public virtual DbSet<Najdobarpsihologsobaremedendodadentermin> Najdobarpsihologsobaremedendodadentermins { get; set; }

    public virtual DbSet<Ocenka> Ocenkas { get; set; }

    public virtual DbSet<Pacient> Pacients { get; set; }

    public virtual DbSet<PacientiZakazanoPovekeOd1TerminImeZapocnuvaSoSlaKojI> PacientiZakazanoPovekeOd1TerminImeZapocnuvaSoSlaKojIs { get; set; }

    public virtual DbSet<Plaka> Plakas { get; set; }

    public virtual DbSet<Plakanje> Plakanjes { get; set; }

    public virtual DbSet<Psiholog> Psihologs { get; set; }

    public virtual DbSet<Psiholoziprezimekoiimeprezimepacientikojrazgleduvaatuslugikakop> Psiholoziprezimekoiimeprezimepacientikojrazgleduvaatuslugikakops { get; set; }

    public virtual DbSet<Sesija> Sesijas { get; set; }

    public virtual DbSet<Termin> Termins { get; set; }

    public virtual DbSet<Terminivotekovenmesecigododgradprilepmailimaidvremepovekeodcasi> Terminivotekovenmesecigododgradprilepmailimaidvremepovekeodcasis { get; set; }

    public virtual DbSet<Usluga> Uslugas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;port=5432;Database=mojPsiholog;Username=postgres;Password=lotr1010");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DavaOcenka>(entity =>
        {
            entity.HasKey(e => new { e.Korisnickoimepacient, e.Korisnickoimepsiholog, e.IdOcenka }).HasName("dava_ocenka_pkey");

            entity.ToTable("dava_ocenka");

            entity.Property(e => e.Korisnickoimepacient)
                .HasColumnType("character varying")
                .HasColumnName("korisnickoimepacient");
            entity.Property(e => e.Korisnickoimepsiholog)
                .HasColumnType("character varying")
                .HasColumnName("korisnickoimepsiholog");
            entity.Property(e => e.IdOcenka).HasColumnName("id_ocenka");

            entity.HasOne(d => d.IdOcenkaNavigation).WithMany(p => p.DavaOcenkas)
                .HasForeignKey(d => d.IdOcenka)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dava_ocenka_id_ocenka_fkey");

            entity.HasOne(d => d.KorisnickoimepacientNavigation).WithMany(p => p.DavaOcenkas)
                .HasForeignKey(d => d.Korisnickoimepacient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dava_ocenka_korisnickoimepacient_fkey");

            entity.HasOne(d => d.KorisnickoimepsihologNavigation).WithMany(p => p.DavaOcenkas)
                .HasForeignKey(d => d.Korisnickoimepsiholog)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dava_ocenka_korisnickoimepsiholog_fkey");
        });

        modelBuilder.Entity<KorisniciCijSessiSeOdrzalePred1GodinaISegasnataImeEF>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("korisnici_cij_sessi_se_odrzale_pred1_godina_i_segasnata_ime_e_f");

            entity.Property(e => e.Korisnickoime)
                .HasColumnType("character varying")
                .HasColumnName("korisnickoime");
        });

        modelBuilder.Entity<Korisnik>(entity =>
        {
            entity.HasKey(e => e.Korisnickoime).HasName("korisnik_pkey");

            entity.ToTable("korisnik");

            entity.Property(e => e.Korisnickoime)
                .HasColumnType("character varying")
                .HasColumnName("korisnickoime");
            entity.Property(e => e.Grad)
                .HasColumnType("character varying")
                .HasColumnName("grad");
            entity.Property(e => e.Ime)
                .HasColumnType("character varying")
                .HasColumnName("ime");
            entity.Property(e => e.Lozinka)
                .HasColumnType("character varying")
                .HasColumnName("lozinka");
            entity.Property(e => e.Meil)
                .HasColumnType("character varying")
                .HasColumnName("meil");
            entity.Property(e => e.Prezime)
                .HasColumnType("character varying")
                .HasColumnName("prezime");
            entity.Property(e => e.Telefon)
                .HasColumnType("character varying")
                .HasColumnName("telefon");
            entity.Property(e => e.Ulica)
                .HasColumnType("character varying")
                .HasColumnName("ulica");
            entity.Property(e => e.Uloga)
                .HasColumnType("character varying")
                .HasColumnName("uloga");
        });

        modelBuilder.Entity<Najdobarpsihologsobaremedendodadentermin>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("najdobarpsihologsobaremedendodadentermin");

            entity.Property(e => e.Korisnickoime)
                .HasColumnType("character varying")
                .HasColumnName("korisnickoime");
            entity.Property(e => e.Prosek).HasColumnName("prosek");
        });

        modelBuilder.Entity<Ocenka>(entity =>
        {
            entity.HasKey(e => e.IdOcenka).HasName("ocenka_pkey");

            entity.ToTable("ocenka");

            entity.Property(e => e.IdOcenka).HasColumnName("id_ocenka");
            entity.Property(e => e.Ocenka1).HasColumnName("ocenka");
        });

        modelBuilder.Entity<Pacient>(entity =>
        {
            entity.HasKey(e => e.Korisnickoime).HasName("pacient_pkey");

            entity.ToTable("pacient");

            entity.Property(e => e.Korisnickoime)
                .HasColumnType("character varying")
                .HasColumnName("korisnickoime");

            entity.HasOne(d => d.KorisnickoimeNavigation).WithOne(p => p.Pacient)
                .HasForeignKey<Pacient>(d => d.Korisnickoime)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pacient_korisnickoime_fkey");

            entity.HasMany(d => d.IdTermins).WithMany(p => p.Korisnickoimes)
                .UsingEntity<Dictionary<string, object>>(
                    "PacientotZakazuvaTermin",
                    r => r.HasOne<Termin>().WithMany()
                        .HasForeignKey("IdTermin")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("pacientot_zakazuva_termin_id_termin_fkey"),
                    l => l.HasOne<Pacient>().WithMany()
                        .HasForeignKey("Korisnickoime")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("pacientot_zakazuva_termin_korisnickoime_fkey"),
                    j =>
                    {
                        j.HasKey("Korisnickoime", "IdTermin").HasName("pacientot_zakazuva_termin_pkey");
                        j.ToTable("pacientot_zakazuva_termin");
                    });

            entity.HasMany(d => d.IdUslugas).WithMany(p => p.Korisnickoimes)
                .UsingEntity<Dictionary<string, object>>(
                    "PacientRazgleduvaUsluga",
                    r => r.HasOne<Usluga>().WithMany()
                        .HasForeignKey("IdUsluga")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("pacient_razgleduva_usluga_id_usluga_fkey"),
                    l => l.HasOne<Pacient>().WithMany()
                        .HasForeignKey("Korisnickoime")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("pacient_razgleduva_usluga_korisnickoime_fkey"),
                    j =>
                    {
                        j.HasKey("Korisnickoime", "IdUsluga").HasName("pacient_razgleduva_usluga_pkey");
                        j.ToTable("pacient_razgleduva_usluga");
                    });
        });

        modelBuilder.Entity<PacientiZakazanoPovekeOd1TerminImeZapocnuvaSoSlaKojI>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("pacienti_zakazano_poveke_od_1_termin_ime_zapocnuva_so_sla_koj_i");

            entity.Property(e => e.Korisnickoime)
                .HasColumnType("character varying")
                .HasColumnName("korisnickoime");
        });

        modelBuilder.Entity<Plaka>(entity =>
        {
            entity.HasKey(e => new { e.Korisnickoime, e.Id, e.IdTermin }).HasName("plaka_pkey");

            entity.ToTable("plaka");

            entity.Property(e => e.Korisnickoime)
                .HasColumnType("character varying")
                .HasColumnName("korisnickoime");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdTermin).HasColumnName("id_termin");

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.Plakas)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("plaka_id_fkey");

            entity.HasOne(d => d.IdTerminNavigation).WithMany(p => p.Plakas)
                .HasForeignKey(d => d.IdTermin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("plaka_id_termin_fkey");

            entity.HasOne(d => d.KorisnickoimeNavigation).WithMany(p => p.Plakas)
                .HasForeignKey(d => d.Korisnickoime)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("plaka_korisnickoime_fkey");
        });

        modelBuilder.Entity<Plakanje>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("plakanje_pkey");

            entity.ToTable("plakanje");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Suma).HasColumnName("suma");
        });

        modelBuilder.Entity<Psiholog>(entity =>
        {
            entity.HasKey(e => e.Korisnickoime).HasName("psiholog_pkey");

            entity.ToTable("psiholog");

            entity.Property(e => e.Korisnickoime)
                .HasColumnType("character varying")
                .HasColumnName("korisnickoime");

            entity.HasOne(d => d.KorisnickoimeNavigation).WithOne(p => p.Psiholog)
                .HasForeignKey<Psiholog>(d => d.Korisnickoime)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("psiholog_korisnickoime_fkey");

            entity.HasMany(d => d.IdUslugas).WithMany(p => p.KorisnickoimesNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "PsihologNudiUsluga",
                    r => r.HasOne<Usluga>().WithMany()
                        .HasForeignKey("IdUsluga")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("psiholog_nudi_usluga_id_usluga_fkey"),
                    l => l.HasOne<Psiholog>().WithMany()
                        .HasForeignKey("Korisnickoime")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("psiholog_nudi_usluga_korisnickoime_fkey"),
                    j =>
                    {
                        j.HasKey("Korisnickoime", "IdUsluga").HasName("psiholog_nudi_usluga_pkey");
                        j.ToTable("psiholog_nudi_usluga");
                    });
        });

        modelBuilder.Entity<Psiholoziprezimekoiimeprezimepacientikojrazgleduvaatuslugikakop>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("psiholoziprezimekoiimeprezimepacientikojrazgleduvaatuslugikakop");

            entity.Property(e => e.Ime)
                .HasColumnType("character varying")
                .HasColumnName("ime");
            entity.Property(e => e.Korisnickoime)
                .HasColumnType("character varying")
                .HasColumnName("korisnickoime");
            entity.Property(e => e.Prezime)
                .HasColumnType("character varying")
                .HasColumnName("prezime");
        });

        modelBuilder.Entity<Sesija>(entity =>
        {
            entity.HasKey(e => e.IdSesija).HasName("sesija_pkey");

            entity.ToTable("sesija");

            entity.Property(e => e.IdSesija).HasColumnName("id_sesija");
            entity.Property(e => e.Broj)
                .HasColumnType("character varying")
                .HasColumnName("broj");
            entity.Property(e => e.Ulica)
                .HasColumnType("character varying")
                .HasColumnName("ulica");
            entity.Property(e => e.Vremetraenje).HasColumnName("vremetraenje");

            entity.HasMany(d => d.IdTermins).WithMany(p => p.IdSesijas)
                .UsingEntity<Dictionary<string, object>>(
                    "SesijaSeOdrzuvaVoTermin",
                    r => r.HasOne<Termin>().WithMany()
                        .HasForeignKey("IdTermin")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("sesija_se_odrzuva_vo_termin_id_termin_fkey"),
                    l => l.HasOne<Sesija>().WithMany()
                        .HasForeignKey("IdSesija")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("sesija_se_odrzuva_vo_termin_id_sesija_fkey"),
                    j =>
                    {
                        j.HasKey("IdSesija", "IdTermin").HasName("sesija_se_odrzuva_vo_termin_pkey");
                        j.ToTable("sesija_se_odrzuva_vo_termin");
                    });
        });

        modelBuilder.Entity<Termin>(entity =>
        {
            entity.HasKey(e => e.IdTermin).HasName("termin_pkey");

            entity.ToTable("termin");

            entity.Property(e => e.IdTermin).HasColumnName("id_termin");
            entity.Property(e => e.Datum).HasColumnName("datum");
            entity.Property(e => e.Grad)
                .HasColumnType("character varying")
                .HasColumnName("grad");
            entity.Property(e => e.Korisnickoime)
                .HasColumnType("character varying")
                .HasColumnName("korisnickoime");
            entity.Property(e => e.Vreme).HasColumnName("vreme");

            entity.HasOne(d => d.KorisnickoimeNavigation).WithMany(p => p.Termins)
                .HasForeignKey(d => d.Korisnickoime)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("termin_korisnickoime_fkey");
        });

        modelBuilder.Entity<Terminivotekovenmesecigododgradprilepmailimaidvremepovekeodcasi>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("terminivotekovenmesecigododgradprilepmailimaidvremepovekeodcasi");

            entity.Property(e => e.Datum).HasColumnName("datum");
            entity.Property(e => e.Ime)
                .HasColumnType("character varying")
                .HasColumnName("ime");
        });

        modelBuilder.Entity<Usluga>(entity =>
        {
            entity.HasKey(e => e.IdUsluga).HasName("usluga_pkey");

            entity.ToTable("usluga");

            entity.Property(e => e.IdUsluga).HasColumnName("id_usluga");
            entity.Property(e => e.Ime)
                .HasColumnType("character varying")
                .HasColumnName("ime");
            entity.Property(e => e.Opis)
                .HasColumnType("character varying")
                .HasColumnName("opis");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
