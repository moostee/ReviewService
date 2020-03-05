using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReviewsService_Core.Migrations
{
    public partial class AddAppClientsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppClients",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    RecordStatus = table.Column<int>(nullable: false),
                    AppId = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    ClientSecret = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppClients", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppClients");
        }
    }
}
