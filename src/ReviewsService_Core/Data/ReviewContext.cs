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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_options == null)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=reviewsdb;Integrated Security=True;");
            }
        }
    }
}
