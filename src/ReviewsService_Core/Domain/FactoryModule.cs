using ReviewsService_Core.Domain.Factory;

namespace ReviewsService_Core.Domain
{
    public class FactoryModule : IFactoryModule
    {
        private ClientFactory _client;
        public ClientFactory Clients { get { if (_client == null) { _client = new ClientFactory(); } return _client; } }
    }
}
