using NPoco;
using NPoco.FluentMappings;
using ReviewsService_Core.Domain.Entity;
using ReviewsService_Core.Domain.Model;
using System.Data.SqlClient;

namespace ReviewsService_Core.Data
{
    public static class ReviewPoco
    {
        /// <summary>
        /// 
        /// </summary>
        public static DatabaseFactory DbFactory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public static void Setup(string config)
        {
            var fluentConfig = FluentMappingConfiguration.Configure(new ReviewsServiceMappings());
            DbFactory = DatabaseFactory.Config(x =>
            {
                x.UsingDatabase(() => new NPoco.Database(config, DatabaseType.SqlServer2012, SqlClientFactory.Instance));
                x.WithFluentConfig(fluentConfig);
            });
        }
    }

    public class ReviewsServiceMappings : Mappings
    {
        public ReviewsServiceMappings()
        {
            For<Client>()
                .PrimaryKey(x => x.Id)
                .TableName("Clients");

            For<ClientModel>()
                .PrimaryKey(x => x.Id)
                .TableName("clientmodel")
                .Columns(x =>
                {
                    x.Column(y => y.RecordStatusText).Ignore();
                    x.Column(y => y.CreatedAtText).Ignore();
                    x.Column(y => y.UpdatedAtText).Ignore();
                });

            For<App>()
                .PrimaryKey(x => x.Id)
                .TableName("Apps");

            For<AppModel>()
                .PrimaryKey(x => x.Id)
                .TableName("appmodel")
                .Columns(x =>
                {
                    x.Column(y => y.RecordStatusText).Ignore();
                    x.Column(y => y.CreatedAtText).Ignore();
                    x.Column(y => y.UpdatedAtText).Ignore();
                });

            For<ReviewType>()
                .PrimaryKey(x => x.Id)
                .TableName("ReviewTypes");

            For<ReviewTypeModel>()
                .PrimaryKey(x => x.Id)
                .TableName("reviewtypemodel")
                .Columns(x =>
                {
                    x.Column(y => y.RecordStatusText).Ignore();
                    x.Column(y => y.CreatedAtText).Ignore();
                    x.Column(y => y.UpdatedAtText).Ignore();
                });

            For<Review>()
                .PrimaryKey(x => x.Id)
                .TableName("Reviews");

            For<ReviewModel>()
                .PrimaryKey(x => x.Id)
                .TableName("reviewmodel")
                .Columns(x =>
                {
                    x.Column(y => y.RecordStatusText).Ignore();
                    x.Column(y => y.CreatedAtText).Ignore();
                    x.Column(y => y.UpdatedAtText).Ignore();
                });


            For<AppClient>()
                .PrimaryKey(x => x.Id)
                .TableName("AppClients");

            For<AppClientModel>()
                .PrimaryKey(x => x.Id)
                .TableName("appclientmodel")
                .Columns(x =>
                {
                    x.Column(y => y.RecordStatusText).Ignore();
                    x.Column(y => y.CreatedAtText).Ignore();
                    x.Column(y => y.UpdatedAtText).Ignore();
                });

        }
    }
}
