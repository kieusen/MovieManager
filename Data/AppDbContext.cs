using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movie_01.Models;

namespace Movie_01.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // Bỏ tiền tố AspNet trong các bảng tạo tự động
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName() ?? "";
                if (tableName.StartsWith("AspNet")) 
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }   

            // Tao khoa Movie_Actor
            builder.Entity<Movie_Actor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });

            builder.Entity<Movie_Actor>()
                .HasOne(ma => ma.Movie)
                .WithMany(m => m.Movies_Actors)
                .HasForeignKey(ma => ma.MovieId);
            
            builder.Entity<Movie_Actor>()
                .HasOne(ma => ma.Actor)
                .WithMany(a => a.Movies_Actors)
                .HasForeignKey(ma => ma.ActorId);
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Movie_Actor> Movies_Actors { get; set; }      
        public DbSet<Order> Orders { get; set; } 
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

    }
}