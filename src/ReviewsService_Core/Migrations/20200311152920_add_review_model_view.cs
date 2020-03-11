using Microsoft.EntityFrameworkCore.Migrations;

namespace ReviewsService_Core.Migrations
{
    public partial class add_review_model_view : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var txt = @"CREATE OR ALTER VIEW [dbo].[reviewmodel]
 AS
 SELECT x.*,
    (SELECT Distinct COUNT(*) From ReviewVotes c  WHERE x.Id = c.ReviewId  AND ReviewVoteTypeId = 1 AND IsActive = 1) as ReviewUpVotes,
	(SELECT Distinct COUNT(*) From ReviewVotes c WHERE x.Id = c.ReviewId AND ReviewVoteTypeId = 2 AND IsActive = 1) as ReviewDownVotes  
   FROM Reviews x
  WHERE x.RecordStatus <> 3 AND x.RecordStatus <> 4";
            migrationBuilder.Sql(txt);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
