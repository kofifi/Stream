using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stream.Models;
using Stream.Areas.Identity.Data;

namespace Stream.Data
{
    public class ApplicationDbContext : IdentityDbContext<StreamUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Library> Libraries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Library>()
                .HasOne(l => l.User)
                .WithMany(u => u.Libraries)
                .HasForeignKey(l => l.UserId);

            modelBuilder.Entity<Library>()
                .HasOne(l => l.Game)
                .WithMany()
                .HasForeignKey(l => l.GameId);

            modelBuilder.Seed();
        }
    }
}