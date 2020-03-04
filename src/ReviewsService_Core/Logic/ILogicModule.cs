namespace ReviewsService_Core.Logic
{
    public interface ILogicModule
    {
        ClientLogic Clients { get; }

        AppLogic AppLogic { get; }
    }
}