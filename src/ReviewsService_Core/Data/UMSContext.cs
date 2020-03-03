using Microsoft.EntityFrameworkCore;
using ReviewsService_Core.Domain.Entity;

namespace ReviewsService_Core.Data
{

    public class UMSContext : DbContext
    {
        private DbContextOptions<UMSContext> _options;
        public UMSContext(DbContextOptions<UMSContext> options)
        : base(options)
        {
            _options = options;
        }

        public UMSContext()
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
