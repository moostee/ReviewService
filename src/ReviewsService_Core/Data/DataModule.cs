using ReviewsService_Core.Domain;

namespace ReviewsService_Core.Data
{
    public class DataModule : IDataModule
    {

        private readonly ReviewContext _context;
        private readonly IFactoryModule _factory;

        public DataModule(ReviewContext context, IFactoryModule factory)
        {
            _context = context;
            _factory = factory;
        }

        private ClientRepository _clients;
        public ClientRepository Clients { get { if (_clients == null) { _clients = new ClientRepository(_context); } return _clients; } }

        private AppRepository _apps;
        public AppRepository Apps { get { if (_apps == null) { _apps = new AppRepository(_context); } return _apps; } }

    }
}
