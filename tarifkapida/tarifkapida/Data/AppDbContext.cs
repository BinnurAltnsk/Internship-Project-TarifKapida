using Microsoft.EntityFrameworkCore;
using tarifkapida.Models;

namespace tarifkapida.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TarifKapida;Trusted_Connection=True;");
            }
        }

        public virtual DbSet<Recipe> RECIPE { get; set; }
        public virtual DbSet<Review> REVIEW { get; set; }
        public virtual DbSet<Users> USER { get; set; }
    }
}
