using ReviewsService_Core.Data.ReviewService;
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

        private ReviewTypeRepository _reviewtypes;
        public ReviewTypeRepository ReviewTypes { get { if (_reviewtypes == null) { _reviewtypes = new ReviewTypeRepository(_context); } return _reviewtypes; } }

        private ReviewRepository _reviews;
        public ReviewRepository Reviews { get { if (_reviews == null) { _reviews = new ReviewRepository(_context); } return _reviews; } }

        private AppClientRepository _appClients;
        public AppClientRepository AppClients { get { if (_appClients == null) { _appClients = new AppClientRepository(_context); } return _appClients; } }

        private ReviewVoteTypeRepository _reviewvotetypes;
        public ReviewVoteTypeRepository ReviewVoteTypes { get { if (_reviewvotetypes == null) { _reviewvotetypes = new ReviewVoteTypeRepository(_context); } return _reviewvotetypes; } }

    }
}
