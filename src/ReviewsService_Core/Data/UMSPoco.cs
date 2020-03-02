using NPoco;
using NPoco.FluentMappings;
using System.Data.SqlClient;

namespace ReviewsService_Core.Data
{
    public static class UMSPoco
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

        }
    }
}
