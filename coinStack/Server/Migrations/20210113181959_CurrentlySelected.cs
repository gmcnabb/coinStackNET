using Microsoft.EntityFrameworkCore.Migrations;

namespace coinStack.Server.Migrations
{
    public partial class CurrentlySelected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CurrentlySelected",
                table: "UserWatchlists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CurrentlySelected",
                table: "UserPortfolios",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentlySelected",
                table: "UserWatchlists");

            migrationBuilder.DropColumn(
                name: "CurrentlySelected",
                table: "UserPortfolios");
        }
    }
}
