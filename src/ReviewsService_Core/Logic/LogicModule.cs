using ReviewsService_Core.Data;
using ReviewsService_Core.Domain;
using ReviewsService_Core.Logic.ReviewService;

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

        private AppLogic _app;
        public AppLogic AppLogic { get { if (_app == null) { _app = new AppLogic(_data, _factory); } return _app; } }

        private ReviewTypeLogic _reviewtype;
        public ReviewTypeLogic ReviewTypeLogic { get { if (_reviewtype == null) { _reviewtype = new ReviewTypeLogic(_data,_factory); } return _reviewtype; } }


    }
}
