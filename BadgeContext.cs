using BadgeMeUp.Models;
using Microsoft.EntityFrameworkCore;

namespace BadgeMeUp
{
    public class BadgeContext:DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Badge>? Badges { get; set;}
        public DbSet<BadgeType>? BadgeTypes { get; set; }
        public DbSet<AssignedBadge>? AssignedBadges { get; set; }

        //public DbSet<AssignedBadge>? AssignedBadges { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=badges1;Trusted_Connection=True;MultipleActiveResultSets=true";

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<AssignedBadge>()
                .HasOne(x => x.User)
                .WithMany(x => x.AssignedBadges)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<AssignedBadge>()
                .HasOne(x => x.Badge);

            modelBuilder
                .Entity<AssignedBadge>()
                .HasOne(x => x.FromUser)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
