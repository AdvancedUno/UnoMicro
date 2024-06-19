using CommandsService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt ) : base(opt)
        {
            
        }

        public DbSet<Uno> Unos { get; set; }
        public DbSet<Command> Commands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Uno>()
                .HasMany(p => p.Commands)
                .WithOne(p => p.Uno!)
                .HasForeignKey(p => p.UnoId);

            modelBuilder
                .Entity<Command>()
                .HasOne(p => p.Uno)
                .WithMany(p => p.Commands)
                .HasForeignKey(p => p.UnoId);

        }
    }
}