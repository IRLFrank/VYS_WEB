using Microsoft.EntityFrameworkCore;
using web_aplikace.Models;

namespace web_aplikace.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Uzivatel> Uzivatele { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Nastavení unikátního indexu pro přezdívku
            modelBuilder.Entity<Uzivatel>()
                .HasIndex(u => u.Prezdivka)
                .IsUnique();
        }
    }
}
