using Microsoft.EntityFrameworkCore;
using UnoService.Models;

namespace UnoService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
            
        }

        public DbSet<Uno> Unos { get; set; }

    


    }
}