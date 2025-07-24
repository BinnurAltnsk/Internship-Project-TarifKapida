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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Review - Recipe ilişkisi
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Recipe)
                .WithMany(r => r.Reviews)
                .HasForeignKey(r => r.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Review - User ilişkisi
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Recipe - User ilişkisi
            modelBuilder.Entity<Recipe>()
                .HasOne(r => r.User)
                .WithMany(u => u.Recipes)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.SetNull); // User silinirse Recipe'yi silme, UserId'yi null yap

            // Recipe - Category ilişkisi
            modelBuilder.Entity<Recipe>()
                .HasOne(r => r.Category)
                .WithMany(c => c.Recipes)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Kategori silinirse tarifler silinmesin

            // Users - UserProfile birebir ilişkisi
            modelBuilder.Entity<Users>()
                .HasOne(u => u.UserProfile)
                .WithOne(up => up.User)
                .HasForeignKey<UserProfile>(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Favorite ilişkisi (User-Recipe)
            modelBuilder.Entity<Favorite>()
                .HasKey(f => f.Id);
            modelBuilder.Entity<Favorite>()
                .HasIndex(f => new { f.UserId, f.RecipeId }).IsUnique();
            modelBuilder.Entity<Favorite>()
                .HasOne<Users>()
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Favorite>()
                .HasOne<Recipe>()
                .WithMany()
                .HasForeignKey(f => f.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TarifKapida;Trusted_Connection=True;");
            }
        }

        public DbSet<Recipe> RECIPE { get; set; }
        public DbSet<Review> REVIEW { get; set; }
        public DbSet<Users> USER { get; set; }
        public DbSet<Favorite> FAVORITE { get; set; }
        public DbSet<Category> CATEGORY { get; set; }
        public DbSet<UserProfile> USERPROFILE { get; set; }
    }
}
