using Microsoft.EntityFrameworkCore;
using ReviewsService_Core.Domain.Entity;

namespace ReviewsService_Core.Data
{

    public class ReviewContext : DbContext
    {
        private DbContextOptions<ReviewContext> _options;
        public ReviewContext(DbContextOptions<ReviewContext> options)
        : base(options)
        {
            _options = options;
        }

        public ReviewContext()
        {

        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<App> Apps { get; set; }
        public DbSet<ReviewType> ReviewTypes { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public DbSet<AppClient> AppClients { get; set; }

        public DbSet<ReviewVoteType> ReviewVoteTypes { get; set; }

        public DbSet<ReviewVote> ReviewVotes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_options == null)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=reviewsdb;Integrated Security=True;");
            }
        }
    }
}
