using Microsoft.EntityFrameworkCore.Migrations;

namespace ReviewsService_Core.Migrations
{
    public partial class add_table_entity_views : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var txt = @"CREATE OR ALTER VIEW [dbo].[appclientmodel]
AS
SELECT Id, CreatedAt, UpdatedAt, RecordStatus, AppId, ClientId, ClientSecret
FROM     dbo.AppClients
WHERE  (RecordStatus <> 3) AND (RecordStatus <> 4)";
            migrationBuilder.Sql(txt);

            var txt1 = @"CREATE OR ALTER VIEW [dbo].[appmodel]
AS
SELECT Id, CreatedAt, UpdatedAt, RecordStatus, Name, Description
FROM     dbo.Apps
WHERE  (RecordStatus <> 3) AND (RecordStatus <> 4)";
            migrationBuilder.Sql(txt1);

            var txt2 = @"CREATE OR ALTER VIEW [dbo].[clientmodel]
AS
SELECT Id, CreatedAt, UpdatedAt, RecordStatus, Name
FROM     dbo.Clients
WHERE  (RecordStatus <> 3) AND (RecordStatus <> 4)
GO";
            migrationBuilder.Sql(txt2);

            var txt3 = @"CREATE OR ALTER VIEW [dbo].[reviewtypemodel]
AS
SELECT Id, CreatedAt, UpdatedAt, RecordStatus, Name
FROM     dbo.ReviewTypes
WHERE  (RecordStatus <> 3) AND (RecordStatus <> 4)";
            migrationBuilder.Sql(txt3);

            var txt4 = @"CREATE OR ALTER VIEW [dbo].[reviewvotemodel]
AS
SELECT Id, CreatedAt, UpdatedAt, RecordStatus, ReviewId, UserId, ReviewVoteTypeId, IsActive
FROM     dbo.ReviewVotes
WHERE  (RecordStatus <> 3) AND (RecordStatus <> 4)";
            migrationBuilder.Sql(txt4);

            var txt5 = @"CREATE OR ALTER VIEW [dbo].[reviewvotetypemodel]
AS
SELECT Id, CreatedAt, UpdatedAt, RecordStatus, Name
FROM     dbo.ReviewVoteTypes
WHERE  (RecordStatus <> 3) AND (RecordStatus <> 4)";
            migrationBuilder.Sql(txt5);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
