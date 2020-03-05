using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReviewsService_Core.Migrations
{
    public partial class ReviewTypeTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReviewTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    RecordStatus = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewTypes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReviewTypes");
        }
    }
}
