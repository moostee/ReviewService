using ReviewsService_Core.Domain;

namespace ReviewsService_Core.Data
{
    public class DataModule : IDataModule
    {

        private readonly UMSContext _context;
        private readonly IFactoryModule _factory;

        public DataModule(UMSContext context, IFactoryModule factory)
        {
            _context = context;
            _factory = factory;
        }

        private ClientRepository _clients;
        public ClientRepository Clients { get { if (_clients == null) { _clients = new ClientRepository(_context); } return _clients; } }


    }
}
