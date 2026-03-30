using Handball4RO.Models;
using Microsoft.EntityFrameworkCore;


namespace Handball4RO.Data
{
    public class ApplicationDbContext : DbContext
    {
        // constructor pentru efectuarea conexiunii
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // definim tabelele
        public DbSet<User> Users { get; set; }
        public DbSet<Echipa> Echipe { get; set; }
        public DbSet<Competitie> Competitii { get; set; }
        public DbSet<Jucator> Jucatori { get; set; }
        public DbSet<Stire> Stiri { get; set; }
        public DbSet<Meci> Meciuri { get; set; }
        public DbSet<Clasament> Clasamente { get; set; }

        public DbSet<StatisticaJucator> StatisticiJucatori { get; set; }

        // configurari pentru a nu sterge echipele daca stergem un meci
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // meciul are 2 legaturi cu echipa
            modelBuilder.Entity<Meci>()
                .HasOne(m => m.EchipaGazda)
                .WithMany(e => e.MeciuriAcasa)
                .HasForeignKey(m => m.EchipaGazdaId)
                .OnDelete(DeleteBehavior.Restrict); // nu stergem echipa daca stergem meciul

            modelBuilder.Entity<Meci>()
                .HasOne(m => m.EchipaOaspete)
                .WithMany(e => e.MeciuriDeplasare)
                .HasForeignKey(m => m.EchipaOaspeteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}