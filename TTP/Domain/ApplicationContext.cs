using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TTP.Models;

namespace TTP.Domain
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Tournament> Tournaments { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Coach> Coachs { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tournament>().HasKey(t => t.Id);
            modelBuilder.Entity<Tournament>().HasMany(_ => _.Players).WithMany(_ => _.Tournaments).UsingEntity(_ => _.ToTable("TOURNAMENT_PLAYERS"));

            modelBuilder.Entity<Service>().HasKey(t => t.Id);

            modelBuilder.Entity<Player>().HasKey(t => t.Id);

            modelBuilder.Entity<Coach>().HasKey(t => t.Id);
        }
    }
}
