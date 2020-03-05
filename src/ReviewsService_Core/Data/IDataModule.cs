using ReviewsService_Core.Data.ReviewService;

namespace ReviewsService_Core.Data
{
    public interface IDataModule
    {
        ClientRepository Clients { get; }

        AppRepository Apps { get; }

        ReviewTypeRepository ReviewTypes { get; }

        ReviewRepository Reviews { get; }
        AppClientRepository AppClients { get; }

        ReviewVoteTypeRepository ReviewVoteTypes { get; }
    }
}