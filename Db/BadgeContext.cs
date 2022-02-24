using BadgeMeUp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

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
        public DbSet<EmailQueue> EmailQueue => Set<EmailQueue>();

        private readonly IConfiguration _configuration;

        public BadgeContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("badgeDb");

            optionsBuilder.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure();
            });

#if DEBUG
            optionsBuilder.LogTo(Console.WriteLine);
#endif
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
