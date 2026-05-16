using Handball4RO.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Handball4RO.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Echipa> Echipe { get; set; }
        public DbSet<Competitie> Competitii { get; set; }
        public DbSet<Jucator> Jucatori { get; set; }
        public DbSet<Stire> Stiri { get; set; }
        public DbSet<Meci> Meciuri { get; set; }
        public DbSet<Clasament> Clasamente { get; set; }
        public DbSet<StatisticaJucator> StatisticiJucatori { get; set; }

        public DbSet<EchipaFavorita> EchipeFavorite { get; set; }
        public DbSet<JucatorFavorit> JucatoriFavoriti { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Meci>()
                .HasOne(m => m.EchipaGazda)
                .WithMany(e => e.MeciuriAcasa)
                .HasForeignKey(m => m.EchipaGazdaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Meci>()
                .HasOne(m => m.EchipaOaspete)
                .WithMany(e => e.MeciuriDeplasare)
                .HasForeignKey(m => m.EchipaOaspeteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}