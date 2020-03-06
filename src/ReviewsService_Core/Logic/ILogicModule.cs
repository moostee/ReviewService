using ReviewsService_Core.Logic.ReviewService;

namespace ReviewsService_Core.Logic
{
    public interface ILogicModule
    {
        ClientLogic Clients { get; }

        AppLogic AppLogic { get; }

        ReviewTypeLogic ReviewTypeLogic { get; }

        ReviewLogic ReviewLogic { get; }
       AppClientLogic AppClients { get; }

        ReviewVoteTypeLogic ReviewVoteTypes { get; }
        ReviewVoteLogic ReviewVotes { get; }
    }
}