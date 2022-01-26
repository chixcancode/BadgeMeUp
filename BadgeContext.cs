using BadgeMeUp.Models;
using Microsoft.EntityFrameworkCore;

namespace BadgeMeUp
{
    public class BadgeContext:DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Badge>? Badges { get; set;}
        //public DbSet<AssignedBadge>? AssignedBadges { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=badges1;Trusted_Connection=True;MultipleActiveResultSets=true";

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasMany(u => u.AssignedBadges)
                .WithMany(b => b.Users)
                .UsingEntity(j => j.ToTable("AssignedBadges"));
        }
    }
}
