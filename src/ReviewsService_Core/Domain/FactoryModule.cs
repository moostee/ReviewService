using ReviewsService_Core.Domain.Factory;

namespace ReviewsService_Core.Domain
{
    public class FactoryModule : IFactoryModule
    {
        private ClientFactory _client;
        public ClientFactory Clients { get { if (_client == null) { _client = new ClientFactory(); } return _client; } }

        private AppFactory _app;
        public AppFactory Apps { get { if (_app == null) { _app = new AppFactory(); } return _app; } }
        private ReviewTypeFactory _reviewtype;
        public ReviewTypeFactory ReviewTypes { get { if (_reviewtype == null) { _reviewtype = new ReviewTypeFactory(); } return _reviewtype; } }

        private ReviewFactory _review;
        public ReviewFactory Reviews { get { if (_review == null) { _review = new ReviewFactory(); } return _review; } }

    }
}
