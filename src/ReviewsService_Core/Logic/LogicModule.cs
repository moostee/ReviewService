using ReviewsService_Core.Data;
using ReviewsService_Core.Domain;

namespace ReviewsService_Core.Logic
{
    public class LogicModule : ILogicModule
    {
        private IFactoryModule _factory;
        private IDataModule _data;


        public LogicModule(IFactoryModule factory, IDataModule data)
        {
            _factory = factory;
            _data = data;
        }

        private ClientLogic _client;

        public ClientLogic Clients { get { if (_client == null) { _client = new ClientLogic(_data, _factory); } return _client; } }

    }
}
