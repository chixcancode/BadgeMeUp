namespace BadgeMeUp.Badge.API.Data
{
    public class BadgeDbContext : DbContext
    {
        //Why Lambdas?
        //https://docs.microsoft.com/en-us/ef/core/miscellaneous/nullable-reference-types#dbcontext-and-dbset

        public DbSet<User> Users => Set<User>();

        public DbSet<Models.Badge> Badges => Set<Models.Badge>();
       
        public DbSet<BadgeType> BadgeTypes => Set<BadgeType>();
        public DbSet<BadgeAward> BadgeAwards => Set<BadgeAward>();

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    string connectionString = "Server=(localdb)\\mssqllocaldb;Database=badges1;Trusted_Connection=True;MultipleActiveResultSets=true";

        //    optionsBuilder.UseSqlServer(connectionString);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<BadgeAward>()
                .HasOne(x => x.Awardee)
                .WithMany(x => x.Badges)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<BadgeAward>()
                .HasOne(x => x.Badge);

            modelBuilder
                .Entity<BadgeAward>()
                .HasOne(x => x.FromUser)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
