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

        private ReviewLogic _review;
        public ReviewLogic ReviewLogic { get { if (_review == null) { _review = new ReviewLogic(_data,_factory); } return _review; } }
        private AppClientLogic _appClient;

        public AppClientLogic AppClients { get { if (_appClient == null) { _appClient = new AppClientLogic(_data, _factory); } return _appClient; } }

        private ReviewVoteTypeLogic _reviewvotetype;

        public ReviewVoteTypeLogic ReviewVoteTypes { get { if (_reviewvotetype == null) { _reviewvotetype = new ReviewVoteTypeLogic(_data, _factory); } return _reviewvotetype; } }

        private ReviewVoteLogic _reviewvote;
        public ReviewVoteLogic ReviewVotes { get { if (_reviewvote == null) { _reviewvote = new ReviewVoteLogic(_data, _factory); } return _reviewvote; } }
    }
}
