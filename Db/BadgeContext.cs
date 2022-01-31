using BadgeMeUp.Models;
using Microsoft.EntityFrameworkCore;

namespace BadgeMeUp.Db
{
    public class BadgeContext : DbContext
    {
        //Why Lambdas?
        //https://docs.microsoft.com/en-us/ef/core/miscellaneous/nullable-reference-types#dbcontext-and-dbset

        public DbSet<User> Users => Set<User>();
        public DbSet<Badge> Badges => Set<Badge>();
        public DbSet<BadgeType> BadgeTypes => Set<BadgeType>();
        public DbSet<AssignedBadge> AssignedBadges => Set<AssignedBadge>();

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

            //Delete assigned badges when a badge is deleted
            modelBuilder
                .Entity<Badge>()
                .HasMany<AssignedBadge>()
                .WithOne(x => x.Badge)
                .OnDelete(DeleteBehavior.Cascade);
                
                


        }
    }
}
