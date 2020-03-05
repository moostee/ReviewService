namespace ReviewsService_Core.Data
{
    public interface IDataModule
    {
        ClientRepository Clients { get; }

        AppRepository Apps { get; }

        ReviewTypeRepository ReviewTypes { get; }

    }
}