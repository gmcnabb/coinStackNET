using Microsoft.EntityFrameworkCore.Migrations;

namespace coinStack.Server.Migrations
{
    public partial class Coin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Coinid",
                table: "Transactions",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Coinid",
                table: "PortfolioCoins",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Coin",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    symbol = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    image = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    last_updated = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coin", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_Coinid",
                table: "Transactions",
                column: "Coinid");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioCoins_Coinid",
                table: "PortfolioCoins",
                column: "Coinid");

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioCoins_Coin_Coinid",
                table: "PortfolioCoins",
                column: "Coinid",
                principalTable: "Coin",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Coin_Coinid",
                table: "Transactions",
                column: "Coinid",
                principalTable: "Coin",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioCoins_Coin_Coinid",
                table: "PortfolioCoins");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Coin_Coinid",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Coin");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_Coinid",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_PortfolioCoins_Coinid",
                table: "PortfolioCoins");

            migrationBuilder.DropColumn(
                name: "Coinid",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Coinid",
                table: "PortfolioCoins");
        }
    }
}
