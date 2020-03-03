using Microsoft.EntityFrameworkCore;

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_options == null)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=reviewsdb;Integrated Security=True;");
            }
        }
    }
}
