using ReviewsService_Core.Domain.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewsService_Core.Domain
{
    public interface IFactoryModule
    {
        ClientFactory Clients { get; }
        AppFactory Apps { get; }

        ReviewTypeFactory ReviewTypes { get; }
        ReviewFactory Reviews { get; }

        AppClientFactory AppClients { get; }
        ReviewVoteTypeFactory ReviewVoteTypes { get; }
    }
}
