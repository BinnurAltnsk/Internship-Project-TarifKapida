using Microsoft.EntityFrameworkCore;
using tarifkapida.Models;

namespace tarifkapida.Data
{
    public class AppDbContext : DbContext
    {
        internal readonly object UserProfiles;

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
                .OnDelete(DeleteBehavior.Restrict); 
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
            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.Username)
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasMaxLength(100);

                entity.Property(e => e.Bio)
                    .HasMaxLength(500);

                entity.Property(e => e.Location)
                    .HasMaxLength(100);

                entity.Property(e => e.Website)
                    .HasMaxLength(200);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20);

                entity.Property(e => e.ProfileImageBase64)
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                // User ile ilişki
                entity.HasOne(e => e.User)
                    .WithOne()
                    .HasForeignKey<UserProfile>(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
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
