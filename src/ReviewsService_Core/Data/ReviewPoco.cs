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

        }
    }
}
